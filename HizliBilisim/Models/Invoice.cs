using System.ComponentModel.DataAnnotations;

namespace HizliBilisim.models;

public class Invoice
{
    public int InvoiceId { get; set; }
    public int CustomerId { get; set; }
    
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime RecordDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
    public Customer Customer { get; set; }

    public ICollection<InvoiceLine> InvoiceLines { get; set; }
}