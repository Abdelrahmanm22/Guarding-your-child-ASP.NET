using AutoMapper;
using GuardingChild.DTOs;
using GuardingChild.Errors;
using GuardingChild.Helpers;
using GuardingChild.Models;
using GuardingChild.Models.Identity;
using GuardingChild.Services.Interfaces;
using GuardingChild.Specifications;
using GuardingChild.UnitOfWorkPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuardingChild.Controllers
{
    public class KidController : APIBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IKidService _kidService;

        public KidController(IUnitOfWork unitOfWork, IMapper mapper, IKidService kidService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _kidService = kidService;
        }
        [Authorize(Roles = UserRoles.Doctor)]
        [HttpGet]
        [ProducesResponseType(typeof(Pagination<KidToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Pagination<KidToReturnDto>>> GetKids([FromQuery]KidSpecParams kidSpec)
        {
            var spec = new KidWithGuardingSpecification(kidSpec);
            var kids = await _unitOfWork.Repository<Kid>().GetAllAsync(spec);
            var kidsDto = _mapper.Map<IReadOnlyList<KidToReturnDto>>(kids);
            var countSpec = new KidWithFiltrationForCountAsync(kidSpec);
            var count = await _unitOfWork.Repository<Kid>().GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<KidToReturnDto>(kidSpec.PageSize,kidSpec.PageIndex,count,kidsDto));
        }

        [Authorize(Roles = UserRoles.Doctor)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(KidToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<KidToReturnDto>> GetKid(int id)
        {
            var spec = new KidWithGuardingSpecification(id);
            var kid = await _unitOfWork.Repository<Kid>().GetByIdAsync(spec);
            if (kid is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var kidDto = _mapper.Map<KidToReturnDto>(kid);
            return Ok(kidDto);
        }

        [Authorize(Roles = UserRoles.Doctor)]
        [HttpPost]
        [ProducesResponseType(typeof(KidToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<KidToReturnDto>> AddKid([FromForm] KidCreateDto model)
        {
            var (kid, errorMessage) = await _kidService.AddKidAsync(model);
            if (errorMessage is not null)
            {
                return BadRequest(new ApiResponse(400, errorMessage));
            }

            var spec = new KidWithGuardingSpecification(kid.Id);
            var createdKid = await _unitOfWork.Repository<Kid>().GetByIdAsync(spec);
            var kidDto = _mapper.Map<KidToReturnDto>(createdKid);
            return Ok(kidDto);
        }

        [Authorize(Roles = UserRoles.Doctor)]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> UpdateKid(int id, [FromForm] KidUpdateDto model)
        {
            var (message, errorMessage) = await _kidService.UpdateKidAsync(id, model);
            if (errorMessage is not null)
            {
                return BadRequest(new ApiResponse(400, errorMessage));
            }

            return Ok(message);
        }

        [Authorize(Roles = UserRoles.Police)]
        [HttpPost("search")]
        [ProducesResponseType(typeof(KidToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<KidToReturnDto>> Search([FromForm] KidSearchDto model)
        {
            var (kid, errorMessage) = await _kidService.SearchAsync(model);
            if (errorMessage is not null)
            {
                return BadRequest(new ApiResponse(400, errorMessage));
            }

            if (kid is null)
            {
                return Ok(null);
            }

            var kidDto = _mapper.Map<KidToReturnDto>(kid);
            return Ok(kidDto);
        }
    }
}
