using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using World.API.Data;
using World.API.DTO.Country;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        //private readonly ApplicationDbContext _DbContext;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger<CountryController> _logger; // used for shows the Error in Console API

        public CountryController(ICountryRepository countryRepository, IMapper mapper, ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            //_DbContext = dbContext;
            _Mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            //var countries = _DbContext.countries.ToList();
            var countries = await _countryRepository.GetAll();

            var countriesDto = _Mapper.Map<List<CountryDto>>(countries);

            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countriesDto);
        }

        [HttpGet("{id=int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            //var country = _DbContext.countries.Find(id);
            var country = await _countryRepository.Get(id);

            if (country == null)
            {
                // This Error will shows when Enter wrong ID in Console API
                _logger.LogError($"Error While Try to get Record id: {id}");   
                return NoContent();
            }
            var countryDto = _Mapper.Map<CountryDto>(country);
            return Ok(countryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult<CreateCountryDto>> Create([FromBody] CreateCountryDto countryDto) // DTO is used for restrict the data to showing unwanted data eg: Id to the user
        {
            //var result = _DbContext.countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == countryDto.Name.ToLower().Trim()).Any();
            var result = _countryRepository.IsRecordExists(x => x.Name == countryDto.Name);

            if (result)
            {
                return Conflict("Country Already Excists in the Database");
            }
            // Local Country Object
            //Country country = new Country();  // country object for asign the data to the dbContext or database
            //country.Name = countryDto.Name;
            //country.ShortName = countryDto.ShortName;
            //country.CountryCode = countryDto.CountryCode;
            var country = _Mapper.Map<Country>(countryDto);     // Above commented codes are simplified in this single line
                                                                //_DbContext.countries.Add(country); // We can not asign the data into the dbContext or database that's why create object for country 
                                                                //_DbContext.SaveChanges();
            await _countryRepository.Create(country);
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        [HttpPut("{id=int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Country>> Update(int id, [FromBody] UpdateCountryDto countryDto)
        {
            if (countryDto == null || id != countryDto.Id)
            {
                return BadRequest();
            }
            //var countryFromDb = _DbContext.countries.Find(Id);
            //if (countryFromDb == null)
            //{
            //    return NotFound();
            //}
            //countryFromDb.Name = country.Name;
            //countryFromDb.ShortName = country.ShortName;
            //countryFromDb.CountryCode = country.CountryCode;
            var country = _Mapper.Map<Country>(countryDto);   // Above commented codes are simplified in this single line
                                                              //_DbContext.countries.Update(country);
                                                              //_DbContext.SaveChanges();
            await _countryRepository.Update(country);
            return NoContent();
        }

        [HttpDelete("{id=int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var country = await _countryRepository.Get(id);

            if (country == null)
            {
                return NotFound();
            }
            //_DbContext.countries.Remove(country);
            //_DbContext.SaveChanges();
            await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
