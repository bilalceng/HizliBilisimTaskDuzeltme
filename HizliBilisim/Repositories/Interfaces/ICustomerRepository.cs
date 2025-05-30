using HizliBilisim.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HizliBilisim.repositories.interfaces
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(Customer customer);
        
        Task<List<Customer>> GetCustomersByUserIdAsync(int userId);
        
        Task<Customer?> GetCustomerByIdAndUserIdAsync(int customerId, int userId);
    }
}