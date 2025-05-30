using HizliBilisim.DTOs;
using HizliBilisim.models;

namespace HizliBilisim.Mappers
{
    public static class InvoiceMapper
    {
        public static InvoiceDto ToDto(this Invoice invoice)
        {
            return new InvoiceDto
            {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                RecordDate = invoice.RecordDate,
                UserId = invoice.UserId
            };
        }
        
        public static Invoice ToEntity(this InvoiceCreateDto dto)
        {
            return new Invoice
            {
                CustomerId = dto.CustomerId,
                InvoiceNumber = dto.InvoiceNumber,
                InvoiceDate = dto.InvoiceDate,
                TotalAmount = dto.TotalAmount,
                RecordDate = dto.RecordDate,
                UserId = dto.UserId,
                InvoiceLines = new List<InvoiceLine>()
            };
        }

        public static Invoice ToEntity(this InvoiceDto dto)
        {
            return new Invoice
            {
                InvoiceId = dto.InvoiceId,      
                CustomerId = dto.CustomerId,
                InvoiceNumber = dto.InvoiceNumber,
                InvoiceDate = dto.InvoiceDate,
                TotalAmount = dto.TotalAmount,
                RecordDate = dto.RecordDate,
                UserId = dto.UserId,
                InvoiceLines = new List<InvoiceLine>() 
            };
        }

    }
}