using eCommerce.SharedLibrary.Response;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.DTOs;
using OrderApi.Application.DTOs.Conversions;
using OrderApi.Application.Interfaces;
using OrderApi.Application.Services;
namespace OrderApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrder orderInterface ,IOrderService orderService): ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await orderInterface.GetAllAsync();
            if (!orders.Any())
                return NotFound("No order detected in the database");
            var (_,List) = OrderConversion.FromEntity(null, orders);
            return !List!.Any() ? NotFound():Ok(List);


        }

        [HttpGet("id{id:int}")]
        public  async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await orderInterface.FindByIdAsync( id);
            if (order is null)
                return NotFound(null);
            var (_order, _) = OrderConversion.FromEntity(order,null);
            return Ok(_order);

        }

        [HttpGet("client/{clentId:int}")]
        public async Task<ActionResult<OrderDTO>> GetClientOrders(int clienId)
        {
            if (clienId <= 0) return BadRequest("Invalid data provided");
            //var orders = await orderInterface.GetOrdersAsync(o=>o.ClientId == clienId);
            var orders = await orderService.GetOrdersByClientId(clienId);
            return !orders.Any() ? NotFound(null) : Ok(orders);
        }

        [HttpGet("details/{orderId:int}")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderDetails(int orderId)
        {
            if (orderId <= 0) return BadRequest("Invalid data provided");
            var orderDetail = await orderService.GetOrderDetails(orderId);
            return orderDetail.OrderId > 0 ? Ok(orderDetail) : NotFound("No Order Found");
        }


        [HttpPost]
        public async Task<ActionResult<Response>>CreateOrder(OrderDTO orderDTO)
        {
            //Check model state if all data annotations are passed.
            if(!ModelState.IsValid)
                return BadRequest("Incomplete data submitted");

            //convert to entity

            var getEntity = OrderConversion.ToEntity(orderDTO);
            var response = await orderInterface.CreateAsync(getEntity);
            return response.Flag ? Ok(response) : BadRequest(response);

        }
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateOrder(OrderDTO orderDTO)
        {
            //convert from dto to entity

            var order = OrderConversion.ToEntity(orderDTO);
            var response= await orderInterface.UpdateAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);

        }

        [HttpDelete]
        
        public async Task<ActionResult<Response>> DeleteOrder(OrderDTO orderDTO)
        {
            //Convert from DTO to entity
            var order = OrderConversion.ToEntity(orderDTO);
            var response = await orderInterface.DeleteAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);


        }

    }
}
