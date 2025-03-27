using RestaurantApi.Models;
using System.Security.Claims;

namespace RestaurantApi.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        PagedResult<RestaurantDto> GetAll(RestaurantQuery restaurantQuery);
        RestaurantDto GetById(int id);
        void Update(int id, EditRestaurantDto dto);
    }
}