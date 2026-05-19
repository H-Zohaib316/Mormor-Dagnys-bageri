using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MormorBageri.DTOs.Customers;
using MormorBageri.interfaces;

namespace MormorBageri.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> ListAllCustomers()
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.ListAllCustomers();
                return Ok(new{Success = true, StatusCode = 200, Items= customers.Count, Data= customers});
            }
            catch
            {   
                return StatusCode(500, "Ett server fel inträffade");

            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindCustomer(int id)
        {
            try
            {
                var customer = await unitOfWork.CustomerRepository.FindCustomer(id);
                if (customer is not null)
                {
                return Ok(new{Success = true, StatusCode = 200, Items= 1, Data= customer});
                    
                }
                return NotFound();
            }
            catch 
            {
                
                return StatusCode(500, "Ett server fel inträffade");
            }
        }

        [HttpGet("products")]
        public async Task<ActionResult> ListCustomerProducts()
        {
            try
            {
            var customerProduct = await unitOfWork.CustomerRepository.ListCustomerProducts();

            if (customerProduct is null) return NotFound();

            return Ok(new{Success = true, StatusCode = 200, Items= customerProduct.Count, Data= customerProduct});
            
                
            }
            catch 
            {
                return StatusCode(500, "Ett server fel inträffade");
              
            }
            
        }


        [HttpPost()]
        public async Task<ActionResult> AddCustomer(PostCustomerDto model)
        {
            try
            {
                if (await unitOfWork.CustomerRepository.AddCustomer(model))
                {
                    await unitOfWork.Complete();
                    return StatusCode(201);
                }
                return StatusCode(500, "Ett server fel inträffade");
            }
            catch 
            {
                
                return StatusCode(500, "Ett server fel inträffade");
                
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateContactPerson(int id, PatchCustomerContactDto model)
        {
            try
            {
                if (await unitOfWork.CustomerRepository.UpdateCustomerContactPerson(id, model))
                {
                    await unitOfWork.Complete();
                    return NoContent();
                
                }

                return StatusCode(500, "Något serverfel inträffade");
                
            }
            catch 
            {
                
                return StatusCode(500, "Något serverfel inträffade");
              
            }
        }




    }
}
