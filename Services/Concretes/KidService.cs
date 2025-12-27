using System.Net.Http.Headers;
using System.Text.Json;
using GuardingChild.DTOs;
using GuardingChild.Enums;
using GuardingChild.Models;
using GuardingChild.Services.Interfaces;
using GuardingChild.Specifications;
using GuardingChild.UnitOfWorkPattern;
using Microsoft.AspNetCore.Http;

namespace GuardingChild.Services.Concretes;

public class KidService : IKidService
{
    private const string AddEndpoint = "https://machineapp.azurewebsites.net/Add";
    private const string UpdateEndpoint = "https://machineapp.azurewebsites.net/Update";
    private const string SearchEndpoint = "https://machineapp.azurewebsites.net/search";
    private const double SearchAccuracyThreshold = 0.90;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpClientFactory _httpClientFactory;

    public KidService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
    {
        _unitOfWork = unitOfWork;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(Kid? Kid, string? ErrorMessage)> AddKidAsync(KidCreateDto model)
    {
        if (model is null)
        {
            return (null, "Invalid kid data.");
        }

        Guardian? guardian = null;
        if (model.GuardianId.HasValue)
        {
            guardian = await _unitOfWork.Repository<Guardian>().GetByIdAsync(model.GuardianId.Value);
            if (guardian is null)
            {
                return (null, "Guardian not found.");
            }
        }
        else
        {
            if (model.Guardian is null)
            {
                return (null, "Guardian data is required when GuardianId is not provided.");
            }

            guardian = CreateGuardian(model.Guardian);
        }

        var lastIndex = await _unitOfWork.KidRepository.GetLastIndexAsync();
        var newIndex = lastIndex + 1;
        var genderValue = MapGenderValue(model.Gender);

        var addSucceeded = await SendAddRequestAsync(model.Image, newIndex, genderValue);
        if (!addSucceeded)
        {
            return (null, "Failed to register biometric data.");
        }

        var kid = new Kid
        {
            Index = newIndex,
            SSN = model.SSN,
            First_Name = model.First_Name,
            Last_Name = model.Last_Name,
            Gender = model.Gender,
            BirthDate = model.BirthDate
        };

        if (model.GuardianId.HasValue)
        {
            kid.GuardianId = model.GuardianId.Value;
            kid.Guardian = guardian;
        }
        else
        {
            kid.Guardian = guardian;
        }

        await _unitOfWork.Repository<Kid>().AddAsync(kid);
        await _unitOfWork.CompleteAsync();

        return (kid, null);
    }

    public async Task<(string? Message, string? ErrorMessage)> UpdateKidAsync(int id, KidUpdateDto model)
    {
        if (model is null)
        {
            return (null, "Invalid kid data.");
        }

        var spec = new KidWithGuardingSpecification(id);
        var kid = await _unitOfWork.Repository<Kid>().GetByIdAsync(spec);
        if (kid is null)
        {
            return (null, "Kid not found.");
        }

        var message = "Kid updated successfully.";

        if (model.Image is not null)
        {
            var updateSucceeded = await SendUpdateRequestAsync(model.Image, kid.Index);
            if (!updateSucceeded)
            {
                return (null, "Failed to update biometric data.");
            }

            message += " Biometric data updated.";
        }
        kid.SSN = model.SSN;
        kid.First_Name = model.First_Name;
        kid.Last_Name = model.Last_Name;
        kid.Gender = model.Gender;
        kid.BirthDate = model.BirthDate;

        if (model.GuardianId.HasValue)
        {
            var guardian = await _unitOfWork.Repository<Guardian>().GetByIdAsync(model.GuardianId.Value);
            if (guardian is null)
            {
                return (null, "Guardian not found.");
            }

            kid.GuardianId = model.GuardianId.Value;
            kid.Guardian = guardian;
        }
        else if (model.Guardian is not null)
        {
            kid.Guardian = CreateGuardian(model.Guardian);
            kid.GuardianId = null;
        }

        _unitOfWork.Repository<Kid>().Update(kid);
        await _unitOfWork.CompleteAsync();

        return (message, null);
    }

    public async Task<(Kid? Kid, string? ErrorMessage)> SearchAsync(KidSearchDto model)
    {
        if (model is null || model.Image is null)
        {
            return (null, "Image is required.");
        }

        var (searchResult, errorMessage) = await SendSearchRequestAsync(model.Image);
        if (errorMessage is not null)
        {
            return (null, errorMessage);
        }

        if (searchResult is null)
        {
            return (null, "Invalid search response.");
        }

        if (searchResult.Accuracy < SearchAccuracyThreshold)
        {
            return (null, null);
        }

        var kid = await _unitOfWork.KidRepository.GetByIndexAsync(searchResult.Index);
        if (kid is null)
        {
            return (null, "Kid not found for the predicted index.");
        }

        return (kid, null);
    }

    private async Task<bool> SendAddRequestAsync(IFormFile image, long index, string gender)
    {
        using var content = CreateImageContent(image, index, gender);
        using var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsync(AddEndpoint, content);
        return response.IsSuccessStatusCode;
    }

    private async Task<bool> SendUpdateRequestAsync(IFormFile image, long index)
    {
        using var content = CreateImageContent(image, index);
        using var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsync(UpdateEndpoint, content);
        return response.IsSuccessStatusCode;
    }

    private async Task<(SearchResponse? Result, string? ErrorMessage)> SendSearchRequestAsync(IFormFile image)
    {
        using var content = CreateImageContent(image);
        using var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsync(SearchEndpoint, content);
        if (!response.IsSuccessStatusCode)
        {
            return (null, "Failed to search biometric data.");
        }

        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<SearchResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return (result, null);
    }

    private static MultipartFormDataContent CreateImageContent(IFormFile image, long? index = null, string? gender = null)
    {
        var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(image.OpenReadStream());
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
        content.Add(fileContent, "image", image.FileName);

        if (index.HasValue)
        {
            content.Add(new StringContent(index.Value.ToString()), "index");
        }

        if (!string.IsNullOrWhiteSpace(gender))
        {
            content.Add(new StringContent(gender), "gender");
        }

        return content;
    }

    private static string MapGenderValue(Gender gender)
    {
        return gender switch
        {
            Gender.Male => "M",
            Gender.Female => "F",
            _ => "Unknown"
        };
    }

    private static Guardian CreateGuardian(GuardianCreateDto model)
    {
        return new Guardian
        {
            SSN_Father = model.SSN_Father,
            Father_Name = model.Father_Name,
            SSN_Mother = model.SSN_Mother,
            Mother_Name = model.Mother_Name,
            Address = model.Address,
            Phone = model.Phone
        };
    }

    private sealed class SearchResponse
    {
        public double Accuracy { get; set; }
        public long Index { get; set; }
    }
}
