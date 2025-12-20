using AutoMapper;
using GuardingChild.DTOs;
using GuardingChild.Errors;
using GuardingChild.Helpers;
using GuardingChild.Models;
using GuardingChild.Models.Identity;
using GuardingChild.Repositories.Interfaces;
using GuardingChild.Specifications;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = UserRoles.Doctor)]
        [HttpGet]
        public async Task<ActionResult<Pagination<KidToReturnDto>>> GetKids([FromQuery]KidSpecParams kidSpec)
        {
            var spec = new KidWithGuardingSpecification(kidSpec);
            var kids = await _kidRepository.GetAllAsync(spec);
            var kidsDto = _mapper.Map<IReadOnlyList<KidToReturnDto>>(kids);
            var countSpec = new KidWithFiltrationForCountAsync(kidSpec);
            var count = await _kidRepository.GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<KidToReturnDto>(kidSpec.PageSize,kidSpec.PageIndex,count,kidsDto));
        }

        [Authorize(Roles = UserRoles.Police)]
        [HttpGet("{id}")]
        public async Task<ActionResult<KidToReturnDto>> GetKid(int id)
        {
            var spec = new KidWithGuardingSpecification(id);
            var kid = await _kidRepository.GetByIdAsync(spec);
            if (kid is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var kidDto = _mapper.Map<KidToReturnDto>(kid);
            return Ok(kidDto);
        }
    }
}
