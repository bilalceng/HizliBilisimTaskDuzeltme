using HizliBilisim.DTOs;
using HizliBilisim.Mappers;
using HizliBilisim.repositories.interfaces;
using HizliBilisim.services.interfaces;

namespace HizliBilisim.services.implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> AddCustomerAsync(CustomerDto dto)
        {
            var customer = dto.ToEntity();
            

            await _customerRepository.AddCustomerAsync(customer);
            return customer.CustomerId;
        }

        public async Task<List<CustomerDto>> GetCustomersByUserIdAsync(int userId)
        {
            var customers = await _customerRepository.GetCustomersByUserIdAsync(userId);
            return customers.Select(c => c.ToDto()).ToList();
        }

        public async Task<CustomerDto?> GetCustomerByIdAndUserIdAsync(int customerId, int userId)
        {
            var customer = await _customerRepository.GetCustomerByIdAndUserIdAsync(customerId, userId);
            return customer?.ToDto();
        }
    }
}