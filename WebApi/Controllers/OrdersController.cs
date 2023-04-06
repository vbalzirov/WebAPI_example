using AutoMapper;
using CompanyName.Application.Services.ProductService.Models;
using CompanyName.Application.Services.ProductService.Services;
using CompanyName.Application.WebApi.OrdersApi.Models.Orders.Requests;
using CompanyName.Application.WebApi.ProductApi.Models.Orders.Requests;
using CompanyName.Application.WebApi.ProductApi.Models.Orders.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.Application.WebApi.OrdersApi
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly IOrdersService _service;

        public OrdersController(
            IOrdersService ordersSrvice,
            ILogger<OrdersController> logger,
            IMapper automapper)
        {
            _logger = logger;
            _service = ordersSrvice;
            _mapper = automapper;
        }

        [HttpGet(Name = "GetOrders")]
        public IActionResult Get()
        {
            var list = _service.Get();
            var result = _mapper.Map<IEnumerable<Order>, IEnumerable<GetOrderResponse>>(list);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var order = _service.Get(id);
                return Ok(_mapper.Map<Order, GetOrderResponse>(order));
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
            var order = _mapper.Map<CreateOrderRequest, Order>(request);
            var result = _service.Create(order);

            var orderResponse = _mapper.Map<Order, GetOrderResponse>(result);

            return Ok(orderResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _service.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, UpdateOrderRequest request)
        {
            var order = _mapper.Map<UpdateOrderRequest, Order>(request);
            order.Id = id;

            _service.Update(order);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}