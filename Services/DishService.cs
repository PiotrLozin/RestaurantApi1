using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;

namespace RestaurantApi.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;

            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();

            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _context.Dishes.FirstOrDefault(r => r.Id == dishId);
            if (dish == null || dish.RestaurantId != restaurantId)
                throw new NotFoundException("Dish not found");

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var disDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);
            return disDtos;
        }

        public void RemoveAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            _context.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }

        public void RemoveById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _context.Dishes.FirstOrDefault(r => r.Id == dishId);
            if (dish == null || dish.RestaurantId != restaurantId)
                throw new NotFoundException("Dish not found");

            _context.Remove(dish);
            _context.SaveChanges();
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _context
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");

            return restaurant;
        }
    }
}
