using RestaurantApi.Models;
using System.Security.Claims;

namespace RestaurantApi.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Update(int id, EditRestaurantDto dto);
    }
}