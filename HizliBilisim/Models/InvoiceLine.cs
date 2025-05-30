namespace HizliBilisim.models;

public class InvoiceLine
{
    public int InvoiceLineId { get; set; }
    public int InvoiceId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime RecordDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
    public Invoice Invoice { get; set; }
}