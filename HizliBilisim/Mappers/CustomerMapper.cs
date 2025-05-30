using HizliBilisim.DTOs;
using HizliBilisim.models;

namespace HizliBilisim.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(this Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,   // <-- Add this line
                TaxNumber = customer.TaxNumber,
                Title = customer.Title,
                Address = customer.Address,
                EMail = customer.EMail,
                UserId = customer.UserId
            };
        }

        public static Customer ToEntity(this CustomerDto dto)
        {
            return new Customer
            {
                TaxNumber = dto.TaxNumber,
                Title = dto.Title,
                Address = dto.Address,
                EMail = dto.EMail,
                UserId = dto.UserId,
                RecordDate = DateTime.UtcNow
            };
        }
        
    }
}