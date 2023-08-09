﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Exceptions;
using HotelListingAPI.Models.Country;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesRepository(HotelListingDbContext context,IMapper mapper) : base(context,mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        //public async Task<Country> GetDetails(int id)
        //{
        //    return await _context.Countries.Include(q => q.Hotels)
        //      .FirstOrDefaultAsync(q => q.Id == id);
        //}

        public async Task<CountryDto>GetDetails(int id)
        {
            var country=await _context.Countries.Include(q => q.Hotels)
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id==id);
            if(country==null)
            {
                throw new NotFoundException(nameof(GetDetails), id);
            }
            return country;
        }

       
    }
}
