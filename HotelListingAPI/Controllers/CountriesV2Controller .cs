﻿using System;
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
using Microsoft.AspNetCore.Authorization;
using HotelListingAPI.Exceptions;
using Microsoft.AspNetCore.OData.Query;

namespace HotelListingAPI.Controllers
{
    [Route("api/v{version:apiVersion}countries")]
    [ApiController]
    [ApiVersion("2.0")]

    public class CountriesV2Controller : ControllerBase
    {
        //private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;
        private readonly ILogger<CountriesController> _logger;

        public CountriesV2Controller(IMapper mapper,ICountriesRepository countriesRepository,ILogger<CountriesController>logger)
        {
           // _context = context;
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
            this._logger = logger;
        }

        // GET: api/Countries
        [HttpGet]
        [EnableQuery]
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
                //_logger.LogWarning($"record not found in {nameof(GetCountry)} with id: {id} ");
                //return NotFound();
                throw new NotFoundException(nameof(GetCountry),id);
            }
            var countryDto = _mapper.Map<CountryDto>(country);
            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
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
                //return NotFound();
                throw new NotFoundException(nameof(GetCountry), id);
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
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            //var country = new Country()
            //{
            //    Name = createCountry.Name,
            //    ShortName = createCountry.ShortName
            //};
            var country=_mapper.Map<Country>(createCountryDto);

         
            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();
           await  _countriesRepository.AddAsync(country);
           // await _countriesRepository.UpdateAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //if (_countriesRepository.GetAsync(id) == null)
            //{
            //    return NotFound();
            //}
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                //return NotFound();
                throw new NotFoundException(nameof(GetCountry), id);
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
