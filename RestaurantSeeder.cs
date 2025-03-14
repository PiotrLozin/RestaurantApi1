using Microsoft.Identity.Client;
using RestaurantApi.Entities;

namespace RestaurantApi
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }

            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User",
                },
                new Role()
                {
                    Name = "Manager",
                },
                new Role()
                {
                    Name = "Admin",
                }
            };

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            return new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Bella Italia",
                    Description = "Autentyczna włoska kuchnia",
                    Category = "Włoska",
                    HasDelivery = true,
                    ContactEmail = "kontakt@bellaitalia.pl",
                    ContactNumber = "123-456-789",
                    Address = new Address
                    {
                        City = "Warszawa",
                        Street = "Marszałkowska 10",
                        PostalCode = "00-001"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Pizza Margherita", Description = "Klasyczna pizza z sosem pomidorowym i mozzarellą", Price = 29.99m },
                        new Dish { Name = "Spaghetti Carbonara", Description = "Makaron z boczkiem, jajkiem i parmezanem", Price = 34.99m },
                        new Dish { Name = "Tiramisu", Description = "Tradycyjny włoski deser", Price = 19.99m }
                    }
                },
                new Restaurant
                {
                    Name = "Sushi World",
                    Description = "Najlepsze sushi w mieście",
                    Category = "Japońska",
                    HasDelivery = true,
                    ContactEmail = "kontakt@sushiworld.pl",
                    ContactNumber = "987-654-321",
                    Address = new Address
                    {
                        City = "Kraków",
                        Street = "Floriańska 20",
                        PostalCode = "31-001"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Nigiri łosoś", Description = "Klasyczne nigiri z łososiem", Price = 24.99m },
                        new Dish { Name = "California Roll", Description = "Sushi z paluszkiem krabowym, awokado i ogórkiem", Price = 32.99m },
                        new Dish { Name = "Ramen", Description = "Tradycyjna zupa z makaronem i wieprzowiną", Price = 39.99m }
                    }
                },
                new Restaurant
                {
                    Name = "American Burger",
                    Description = "Soczyste burgery i frytki",
                    Category = "Amerykańska",
                    HasDelivery = false,
                    ContactEmail = "kontakt@americanburger.pl",
                    ContactNumber = "555-666-777",
                    Address = new Address
                    {
                        City = "Wrocław",
                        Street = "Rynek 5",
                        PostalCode = "50-001"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Classic Cheeseburger", Description = "Burger wołowy z serem cheddar", Price = 27.99m },
                        new Dish { Name = "BBQ Ribs", Description = "Żeberka w sosie BBQ", Price = 49.99m },
                        new Dish { Name = "Onion Rings", Description = "Chrupiące krążki cebulowe", Price = 15.99m }
                    }
                }
            };
        }


    }
}
