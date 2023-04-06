using AutoMapper;
using CompanyName.Application.Dal.Orders.Models;
using CompanyName.Application.Services.ProductService.Models;
using CompanyName.Application.WebApi.OrdersApi.Models.Orders;
using CompanyName.Application.WebApi.ProductApi.Models.Orders.Responses;

namespace CompanyName.Application.WebApi.OrdersApi.Mappings
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, GetOrderResponse>()
                .ForMember(src => src.OrderProducts, opt => opt.MapFrom(x => x.Products));

            CreateMap<GetOrderResponse, Order>()
                .ForMember(src => src.Products, opt => opt.MapFrom(x => x.OrderProducts));

            CreateMap<Order, OrderDtoBase>()
                .ForMember(src => src.OrderProducts, opt => opt.MapFrom(x => x.Products));

            CreateMap<OrderDtoBase, Order>()
                .ForMember(src => src.Products, opt => opt.MapFrom(x => x.OrderProducts));

            CreateMap<Order, OrderDal>()
                .ForMember(src => src.OrderProducts, opt => opt.MapFrom(x => x.Products));

            CreateMap<OrderDal, Order>()
                .ForMember(src => src.Products, opt => opt.MapFrom(x => x.OrderProducts.Select(op => op)));

            CreateMap<Product, ProductDal>();
            CreateMap<ProductDal, Product>();

            CreateMap<Product, ProductDto>();

            // Кастомный маппинг полей для Product, в объектк Order, поскольку часть данных хранится в ProductDal,
            // а количество товара в Order, в OrderProductDal.ProductQuantity
            CreateMap<OrderProductDal, Product>()
                .ForMember(src => src.Id, opt => opt.MapFrom(x => x.ProductId))
                .ForMember(src => src.Name, opt => opt.MapFrom(x => x.Product.Name))
                .ForMember(src => src.ProductQuantity, opt => opt.MapFrom(x => x.ProductQuantity));

            CreateMap<ProductDto, Product>();
        }
    }
}
