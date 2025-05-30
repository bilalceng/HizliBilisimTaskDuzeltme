using HizliBilisim.models;

public interface IInvoiceService
{
    Task AddInvoiceAsync(Invoice invoice);
    Task UpdateInvoiceAsync(Invoice invoice);
    Task<bool> DeleteInvoiceAsync(int invoiceId, int userId);
    Task<List<Invoice>> GetInvoicesAsync(DateTime start, DateTime end, int userId);
    Task<Invoice?> GetInvoiceByIdAsync(int id, int userId);
    Task<List<Invoice>> GetAllInvoicesAsync(int userId);
}