using AutoMapper;
using CornerStore.Models;
using CornerStore.Models.DTOs;

namespace CornerStore.Mapping 
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Cashier -> CashierDTO
            CreateMap<Cashier, CashierDTO>()
                .ForMember(dest => dest.OrderIds, opt => opt.MapFrom(src => src.Orders.Select(o => o.Id).ToList()));

            // Order -> OrderDTO
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.OrderProducts.Sum(op => op.Product.Price * op.Quantity)));

            // Product -> ProductDTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
        }
    }
}
