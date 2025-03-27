using Microsoft.AspNetCore.Authorization;
using RestaurantApi.Entities;
using System.Security.Claims;

namespace RestaurantApi.Authorization
{
    public class CreatedMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        private readonly RestaurantDbContext _context;

        public CreatedMultipleRestaurantsRequirementHandler(RestaurantDbContext dbContext)
        {
            _context = dbContext;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CreatedMultipleRestaurantsRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantsCount = _context.Restaurants.Count(r => r.CreatedById == userId);

            if (createdRestaurantsCount >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
