using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using World.API.DTO.States;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _StatesRepository;
        private readonly IMapper _Mapper;

        public StatesController(IStatesRepository statesRepository, IMapper mapper)
        {
            _StatesRepository = statesRepository;
            _Mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetAll()
        {
            var states = await _StatesRepository.GetAll();

            var statesDto = _Mapper.Map<List<StatesDto>>(states);

            if (states == null)
            {
                return NoContent();
            }
            return Ok(statesDto);
        }

        [HttpGet("{id=int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StatesDto>> GetById(int id)
        {
            var state = await _StatesRepository.Get(id);
            var stateDto = _Mapper.Map<StatesDto>(state);

            if (state == null)
            {
                return NoContent();
            }
            return Ok(stateDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult<CreateStatesDto>> Create([FromBody] CreateStatesDto stateDto)
        {
            var result = _StatesRepository.IsRecordExists(x=>x.Name ==stateDto.Name);

            if (result)
            {
                return Conflict("States Already Excists in the Database");
            }
            var states = _Mapper.Map<States>(stateDto);

            await _StatesRepository.Create(states);
            return CreatedAtAction("GetById", new { id = states.Id }, states);
        }

        [HttpPut("{id=int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<States>> Update(int id, [FromBody] UpdateStatesDto stateDto)
        {
            if (stateDto == null || id != stateDto.Id)
            {
                return BadRequest();
            }
            var state = _Mapper.Map<States>(stateDto);

            await _StatesRepository.Update(state);
            return NoContent();
        }

        [HttpDelete("{id=int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var state = await _StatesRepository.Get(id);

            if (state == null)
            {
                return NotFound();
            }
            await _StatesRepository.Delete(state);
            return NoContent();
        }
    }
}
