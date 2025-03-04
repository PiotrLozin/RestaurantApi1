using AutoMapper;
using RestaurantApi.Entities;
using RestaurantApi.Models;

namespace RestaurantApi
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(r => r.City, r => r.MapFrom(re => re.Address.City))
                .ForMember(r => r.Street, r => r.MapFrom(re => re.Address.Street))
                .ForMember(r => r.PostalCode, r => r.MapFrom(re => re.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address
                {
                    City = dto.City,
                    Street = dto.Street,
                    PostalCode = dto.PostalCode
                }));
        }
    }
}
