using HotelListingAPI.Models.Country;
using System.Threading.Tasks;

namespace HotelListingAPI.Contracts
{
    public interface ICountriesRepository :IGenericRepository<Country> 
    {
        //Task<Country> GetDetails(int id);
        Task<CountryDto> GetDetails(int id);
    }
}
