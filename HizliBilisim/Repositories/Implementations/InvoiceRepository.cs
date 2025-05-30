using HizliBilisim.data;
using HizliBilisim.models;
using HizliBilisim.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace HizliBilisim.repositories.implementations;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly ApplicationDbContext _context;

    public InvoiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddInvoiceAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateInvoiceAsync(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteInvoiceAsync(int invoiceId)
    {
        var invoice = await _context.Invoices.FindAsync(invoiceId);
        if (invoice != null)
        {
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(int invoiceId)
    {
        return await _context.Invoices
            .Include(i => i.InvoiceLines)
            .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);
    }

    public async Task<List<Invoice>> GetInvoicesAsync(DateTime startDate, DateTime endDate, int userId)
    {
        return await _context.Invoices
            .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate && i.UserId == userId)
            .Include(i => i.Customer)
            .Include(i => i.InvoiceLines)
            .ToListAsync();
    }

    public async Task<List<Invoice>> GetAllInvoicesByUserAsync(int userId)
    {
        return await _context.Invoices
            .Where(i => i.UserId == userId)
            .Include(i => i.Customer)
            .Include(i => i.InvoiceLines)
            .ToListAsync();
    }
}