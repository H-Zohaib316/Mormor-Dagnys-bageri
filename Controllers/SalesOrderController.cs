using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MormorBageri.DTOs.SalesOrders;
using MormorBageri.interfaces;

namespace MormorBageri.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class SalesOrderController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> ListAllOrders()
        {
            try
            {
                var orders = await unitOfWork.SalesOrderRepository.ListAllOrders();
                return Ok(new { Success = true, StatusCode = 200, Items = orders.Count, Data = orders });


            }
            catch
            {

                return StatusCode(500, "Ett server fel inträffade");

            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindCustomerById(int id)
        {
            try
            {
                var order = await unitOfWork.SalesOrderRepository.FindOrderById(id);
                if (order is not null)
                {
                    return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = order });

                }
                return NotFound("Order hittades ej");

            }
            catch
            {

                return StatusCode(500, "Ett server fel inträffade");

            }




        }

        [HttpGet("ordernumber/{ordernumber}")]
        public async Task<ActionResult> FindCustomerByOrderNumber(string ordernumber)
        {
            try
            {
                var order = await unitOfWork.SalesOrderRepository.FindOrderByOrderNumber(ordernumber);
                if (order is not null)
                {
                    return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = order });
                }
                return NotFound("Order hittades ej");

            }
            catch
            {

                return StatusCode(500, "Ett server fel inträffade");

            }

        }


        [HttpGet("date")]
        public async Task<ActionResult> FindOrderByDate([FromQuery] DateTime date)
        {
            try
            {
                var order = await unitOfWork.SalesOrderRepository.FindOrderByDate(date);
                if (order.Count == 0)
                    return NotFound("Order hittades ej");

                return Ok(new
                {
                    Success = true,
                    StatusCode = 200,
                    Items = order.Count,
                    Data = order
                });


            }
            catch
            {

                return StatusCode(500, "Ett server fel inträffade");

            }

        }

        [HttpPost()]
        public async Task<IActionResult> AddOrder([FromBody] PostSalesOrderDto model)
        {
            try
            {

                if (await unitOfWork.SalesOrderRepository.AddOrder(model))
                {
                    await unitOfWork.Complete();
                    return Ok(new { message = "Order added." });
                }


                return BadRequest("Customer or product was not found.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);


            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {

            if (await unitOfWork.SalesOrderRepository.DeleteOrder(id))
            {
                await unitOfWork.Complete();
                return NoContent();
            }
            return StatusCode(500, "Något server fel inträffade");






        }
    }
}
