using HizliBilisim.DTOs;
using HizliBilisim.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HizliBilisim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = GetCurrentUserId();
            dto.UserId = userId; 

            var customerId = await _customerService.AddCustomerAsync(dto);
            return Ok(customerId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetCurrentUserId();
            var customers = await _customerService.GetCustomersByUserIdAsync(userId);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetCurrentUserId();
            var customer = await _customerService.GetCustomerByIdAndUserIdAsync(id, userId);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
    }
}