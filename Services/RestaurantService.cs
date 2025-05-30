﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Authorization;
using RestaurantApi.Entities;
using RestaurantApi.Exceptions;
using RestaurantApi.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace RestaurantApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantService(
            RestaurantDbContext dbContext,
            IMapper mapper,
            ILogger<RestaurantService> logger,
            IAuthorizationService authorizationService,
            IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public PagedResult<RestaurantDto> GetAll(RestaurantQuery restaurantQuery)
        {
            var baseQuery = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .Where(
                    r => restaurantQuery.SearchPhrase == null
                    || (r.Name.ToLower().Contains(restaurantQuery.SearchPhrase.ToLower())
                    || r.Description.ToLower().Contains(restaurantQuery.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(restaurantQuery.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r => r.Name},
                    { nameof(Restaurant.Description), r => r.Description},
                    { nameof(Restaurant.Category), r => r.Category}
                };

                var selectedColum = columnsSelectors[restaurantQuery.SortBy];

                baseQuery = restaurantQuery.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColum)
                    : baseQuery.OrderByDescending(selectedColum);
            }

            var restaurants = baseQuery
                .Skip(restaurantQuery.PageSize * (restaurantQuery.PageNumber - 1))
                .Take(restaurantQuery.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();
            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            var result = new PagedResult<RestaurantDto>(restaurantDtos, totalItemsCount, restaurantQuery.PageSize, restaurantQuery.PageNumber);
            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = _userContextService.GetUserId;
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} Delete action invoked");

            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(
                _userContextService.User,
                restaurant,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public void Update(int id, EditRestaurantDto dto)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(
                _userContextService.User,
                restaurant,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            restaurant.Name = dto.Name;
            restaurant.HasDelivery = dto.HasDelivery;
            restaurant.Description = dto.Description;


            _dbContext.SaveChanges();

        }
    }
}
