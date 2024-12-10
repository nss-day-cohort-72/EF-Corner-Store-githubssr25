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

            CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.Products, opt =>
                opt.MapFrom(src => src.OrderProducts.Select(op => op.Product)));


            // Product -> ProductDTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<Product, ProductDTO>()
    .ForMember(dest => dest.QuantitySold, opt => 
        opt.MapFrom(src => src.OrderProducts != null ? src.OrderProducts.Sum(op => op.Quantity) : 0));


            CreateMap<CreateProductDTO, Product>();

            // 2️⃣ Product -> ProductDTO
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));


            // 3️⃣ UpdateProductDTO -> Product (for PATCH updates)
            CreateMap<UpdateProductDTO, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Only map non-null values
        
        // CreateMap<UpdateProductDTO, ProductDTO>()
        // .ForMember(dest => dest.QuantitySold, opt => 
        // opt.MapFrom(src => src.Quantity ?? 0));
        //It maps src.Quantity from UpdateProductDTO to dest.QuantitySold in ProductDTO.
        
        }
    }
}
