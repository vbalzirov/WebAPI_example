using AutoMapper;
using CompanyName.Application.Services.ProductService.Models;
using CompanyName.Application.Services.ProductService.Services;
using CompanyName.Application.WebApi.OrdersApi.Models.Orders.Requests;
using CompanyName.Application.WebApi.ProductApi.Models.Orders.Requests;
using CompanyName.Application.WebApi.ProductApi.Models.Orders.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.Application.WebApi.OrdersApi
{
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;
        private readonly IOrdersService service;

        public OrdersController(
            IOrdersService ordersService,
            ILogger<OrdersController> logger,
            IMapper automapper)
        {
            this.logger = logger;
            service = ordersService;
            mapper = automapper;
        }

        [HttpGet(Name = "GetOrders")]
        public IActionResult Get()
        {
            logger.Log(LogLevel.Information, "Get Orders request recieved");

            var list = service.Get();
            var result = mapper.Map<IEnumerable<Order>, IEnumerable<GetOrderResponse>>(list);

            logger.Log(LogLevel.Information, "Get Orders response sent", result);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var order = service.Get(id);
                return Ok(mapper.Map<Order, GetOrderResponse>(order));
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Creates Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            var order = mapper.Map<CreateOrderRequest, Order>(request);
            var result = service.Create(order);

            var orderResponse = mapper.Map<Order, GetOrderResponse>(result);

            return Ok(orderResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            service.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, UpdateOrderRequest request)
        {
            var order = mapper.Map<UpdateOrderRequest, Order>(request);
            order.Id = id;

            service.Update(order);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}