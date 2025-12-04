using GuardingChild.Models;
using GuardingChild.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuardingChild.Controllers
{
    public class KidController : APIBaseController
    {
        private readonly IGenericRepository<Kid> _kidRepository;

        public KidController(IGenericRepository<Kid> kidRepository)
        {
            _kidRepository = kidRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Kid>>> GetKids()
        {
            var kids = await _kidRepository.GetAllAsync();
            return Ok(kids);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kid>> GetKid(int id)
        {
            var kid = await _kidRepository.GetByIdAsync(id);
            if (kid is null)
            {
                return NotFound();
            }
            return Ok(kid);
        }
    }
}
