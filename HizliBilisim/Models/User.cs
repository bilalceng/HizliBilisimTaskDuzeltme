namespace HizliBilisim.models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime RecordDate { get; set; }

    public ICollection<Customer> Customers { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
    public ICollection<InvoiceLine> InvoiceLines { get; set; }
}
