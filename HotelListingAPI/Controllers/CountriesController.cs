using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI;
using HotelListingAPI.Data;
using HotelListingAPI.Models.Country;
using AutoMapper;
using HotelListingAPI.Contracts;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        //private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(IMapper mapper,ICountriesRepository countriesRepository)
        {
           // _context = context;
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
          //if (_context.Countries == null)
          //{
          //    return NotFound();
          //}
            //select * from countries;
            // var countries=await _context.Countries.ToListAsync();
            var countries = await _countriesRepository.GetAllAsync();
            var records=_mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
            //return await _context.Countries.ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            //if (_context.Countries == null)
            //{
            //    return NotFound();
            //}
            //var country = await _context.Countries.Include(q =>q.Hotels)
            //    .FirstOrDefaultAsync(q => q.Id == id);
            var country = await _countriesRepository.GetDetails(id);
            

            if (country == null)
            {
                return NotFound();
            }
            var countryDto = _mapper.Map<CountryDto>(country);
            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("invalid record id");
            }

            // _context.Entry(country).State = EntityState.Modified;
            //var country = await _context.Countries.FindAsync(id);
            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            _mapper.Map(updateCountryDto, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            //var country = new Country()
            //{
            //    Name = createCountry.Name,
            //    ShortName = createCountry.ShortName
            //};
            var country=_mapper.Map<Country>(createCountryDto);

          if (_countriesRepository.GetAsync== null)
          {
              return Problem("Entity set 'HotelListingDbContext.Countries'  is null.");
          }
            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();
           await  _countriesRepository.AddAsync(country);
           // await _countriesRepository.UpdateAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //if (_countriesRepository.GetAsync(id) == null)
            //{
            //    return NotFound();
            //}
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

           await  _countriesRepository.DeleteAsync(id);
            //await _countriesRepository.UpdateAsync(country);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            // return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
            return await _countriesRepository.Exists(id);
        }
    }
}
