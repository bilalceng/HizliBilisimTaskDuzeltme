using HizliBilisim.models;

namespace HizliBilisim.repositories.interfaces;

public interface IInvoiceRepository
{
    Task AddInvoiceAsync(Invoice invoice);
    Task UpdateInvoiceAsync(Invoice invoice);
    Task DeleteInvoiceAsync(int invoiceId);
    Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);
    Task<List<Invoice>> GetInvoicesAsync(DateTime startDate, DateTime endDate, int userId);
    Task<List<Invoice>> GetAllInvoicesByUserAsync(int userId);
}