using HizliBilisim.models;

namespace HizliBilisim.repositories.interfaces;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string userName, string password);
    Task AddUserAsync(User user);
}