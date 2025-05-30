using HizliBilisim.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HizliBilisim.services.interfaces
{
    public interface ICustomerService
    {
        Task<int> AddCustomerAsync(CustomerDto dto);
        Task<List<CustomerDto>> GetCustomersByUserIdAsync(int userId);
        Task<CustomerDto?> GetCustomerByIdAndUserIdAsync(int customerId,int userId);
    }
}