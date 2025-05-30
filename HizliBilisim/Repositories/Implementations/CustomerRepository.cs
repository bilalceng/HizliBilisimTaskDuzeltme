using HizliBilisim.data;
using HizliBilisim.models;
using HizliBilisim.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace HizliBilisim.repositories.implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetCustomersByUserIdAsync(int userId)
        {
            return await _context.Customers
                .Where(c => c.UserId == userId)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAndUserIdAsync(int customerId, int userId)
        {
            return await _context.Customers
                .Where(c => c.CustomerId == customerId && c.UserId == userId)
                .Include(c => c.User)
                .FirstOrDefaultAsync();
        }
    }
}