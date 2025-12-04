using AutoMapper;
using GuardingChild.DTOs;
using GuardingChild.Errors;
using GuardingChild.Models;
using GuardingChild.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuardingChild.Controllers
{
    public class KidController : APIBaseController
    {
        private readonly IGenericRepository<Kid> _kidRepository;
        private readonly IMapper _mapper;

        public KidController(IGenericRepository<Kid> kidRepository,IMapper mapper)
        {
            _kidRepository = kidRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<KidToReturnDto>>> GetKids()
        {
            var kids = await _kidRepository.GetAllAsync();
            var kidsDto = _mapper.Map<IReadOnlyList<KidToReturnDto>>(kids);
            return Ok(kidsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kid>> GetKid(int id)
        {
            var kid = await _kidRepository.GetByIdAsync(id);
            if (kid is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var kidDto = _mapper.Map<KidToReturnDto>(kid);
            return Ok(kidDto);
        }
    }
}
