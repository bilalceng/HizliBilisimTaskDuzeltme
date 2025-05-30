using HizliBilisim.DTOs;
using HizliBilisim.Mappers;
using HizliBilisim.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HizliBilisim.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class InvoiceController : BaseController
{
    private readonly IInvoiceService _invoiceService;
    
    private readonly ILogger<InvoiceController> _logger;

    public InvoiceController(IInvoiceService invoiceService,ILogger<InvoiceController> logger)
    {
        _invoiceService = invoiceService;
        _logger = logger;
    }


    [HttpPost]
    public async Task<IActionResult> AddInvoice([FromBody] InvoiceCreateDto dto)
    {
        var userId = GetCurrentUserId();
        dto.UserId = userId;

        var entity = dto.ToEntity();
        await _invoiceService.AddInvoiceAsync(entity);

        return Ok("Invoice created successfully.");
    }

    
    [HttpPut]
    public async Task<IActionResult> UpdateInvoice([FromBody] InvoiceDto dto)
    {
        await _invoiceService.UpdateInvoiceAsync(dto.ToEntity());
        return Ok("Invoice updated successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetInvoices([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var userId = GetCurrentUserId();
        var invoices = await _invoiceService.GetInvoicesAsync(startDate, endDate, userId);
        return Ok(invoices);
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceById(int id)
    {
        var userId = GetCurrentUserId();
        var invoice = await _invoiceService.GetInvoiceByIdAsync(id, userId);
        if (invoice == null)
            return NotFound($"Invoice with ID {id} not found or access denied.");

        return Ok(invoice.ToDto());
    }
    
    

    [HttpGet("all")]
    public async Task<IActionResult> GetAllInvoices()
    {
        var userId = GetCurrentUserId();
        var invoices = await _invoiceService.GetAllInvoicesAsync(userId);
        var dtos = invoices.Select(i => i.ToDto()).ToList();
        return Ok(dtos); 
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        var userId = GetCurrentUserId();
        var success = await _invoiceService.DeleteInvoiceAsync(id, userId);
        if (!success)
        {
            return NotFound($"Invoice with ID {id} not found or access denied.");
        }
        return Ok("Invoice deleted successfully.");
    }


}
