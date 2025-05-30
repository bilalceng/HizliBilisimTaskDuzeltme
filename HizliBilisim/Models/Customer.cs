using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HizliBilisim.models;

public class Customer
{
    public int CustomerId { get; set; }
    
    public string TaxNumber { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;
    
    public string EMail { get; set; } = string.Empty;
    
    public DateTime RecordDate { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Invoice> Invoices { get; set; }
}