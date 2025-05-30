using HizliBilisim.data;
using HizliBilisim.models;
using HizliBilisim.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace HizliBilisim.repositories.implementations;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserAsync(string userName, string password)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}