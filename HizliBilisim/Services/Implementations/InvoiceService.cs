using HizliBilisim.models;
using HizliBilisim.repositories.interfaces;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(IInvoiceRepository invoiceRepository,ILogger<InvoiceService> logger)
    {
        _invoiceRepository = invoiceRepository;
        _logger = logger;
    }

    public async Task AddInvoiceAsync(Invoice invoice)
    {
        await _invoiceRepository.AddInvoiceAsync(invoice);
    }

    public async Task UpdateInvoiceAsync(Invoice invoice)
    {
        _logger.LogInformation("Starting UpdateInvoiceAsync for InvoiceId: {InvoiceId}", invoice.InvoiceId);

        var existing = await _invoiceRepository.GetInvoiceByIdAsync(invoice.InvoiceId);

        if (existing == null)
        {
            _logger.LogWarning("Invoice with Id {InvoiceId} not found for update.", invoice.InvoiceId);
            return;
        }

        _logger.LogInformation("Current Invoice State before update: {@ExistingInvoice}", existing);
        _logger.LogInformation("New Invoice Data: {@NewInvoice}", invoice);

        // Compare properties and log changes individually
        if (existing.CustomerId != invoice.CustomerId)
            _logger.LogInformation("CustomerId changed from {Old} to {New}", existing.CustomerId, invoice.CustomerId);

        if (existing.InvoiceNumber != invoice.InvoiceNumber)
            _logger.LogInformation("InvoiceNumber changed from {Old} to {New}", existing.InvoiceNumber, invoice.InvoiceNumber);

        if (existing.InvoiceDate != invoice.InvoiceDate)
            _logger.LogInformation("InvoiceDate changed from {Old} to {New}", existing.InvoiceDate, invoice.InvoiceDate);

        if (existing.TotalAmount != invoice.TotalAmount)
            _logger.LogInformation("TotalAmount changed from {Old} to {New}", existing.TotalAmount, invoice.TotalAmount);

        if (existing.RecordDate != invoice.RecordDate)
            _logger.LogInformation("RecordDate changed from {Old} to {New}", existing.RecordDate, invoice.RecordDate);
        
        if (existing.UserId != invoice.UserId)
            _logger.LogInformation("RecordDate changed from {Old} to {New}", existing.UserId, invoice.UserId);


        // Apply update
        existing.CustomerId = invoice.CustomerId;
        existing.InvoiceNumber = invoice.InvoiceNumber;
        existing.InvoiceDate = invoice.InvoiceDate;
        existing.TotalAmount = invoice.TotalAmount;
        existing.RecordDate = invoice.RecordDate;

        await _invoiceRepository.UpdateInvoiceAsync(existing);

        _logger.LogInformation("Invoice with Id {InvoiceId} updated successfully.", invoice.InvoiceId);
    }


    public async Task<bool> DeleteInvoiceAsync(int invoiceId, int userId)
    {
        var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
        if (invoice != null && invoice.UserId == userId)
        {
            await _invoiceRepository.DeleteInvoiceAsync(invoiceId);
            return true;
        }

        return false;
    }

    public async Task<List<Invoice>> GetInvoicesAsync(DateTime start, DateTime end, int userId)
    {
        return await _invoiceRepository.GetInvoicesAsync(start, end, userId);
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(int id, int userId)
    {
        var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
        if (invoice != null && invoice.UserId == userId)
        {
            return invoice;
        }

        return null;
    }

    public async Task<List<Invoice>> GetAllInvoicesAsync(int userId)
    {
        return await _invoiceRepository.GetAllInvoicesByUserAsync(userId);
    }
}
