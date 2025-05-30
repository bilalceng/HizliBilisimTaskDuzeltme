using HizliBilisim.DTOs;

namespace HizliBilisim.services.interfaces;

public interface IAuthService
{
    Task<UserDto?> LoginAsync(string username, string password);
    Task<UserDto?> RegisterAsync(UserForCreationDto dto);
}