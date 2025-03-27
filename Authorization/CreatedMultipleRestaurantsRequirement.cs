using Microsoft.AspNetCore.Authorization;

namespace RestaurantApi.Authorization
{
    public class CreatedMultipleRestaurantsRequirement : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; set; }

        public CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }
    }
}
