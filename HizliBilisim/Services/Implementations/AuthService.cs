using HizliBilisim.DTOs;
using HizliBilisim.models;
using HizliBilisim.repositories.interfaces;
using HizliBilisim.services.interfaces;

namespace HizliBilisim.services.implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetUserAsync(username, password);
        if (user == null) return null;

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            RecordDate = user.RecordDate
        };
    }
    
    
    public async Task<UserDto?> RegisterAsync(UserForCreationDto dto)
    {
        var existingUser = await _userRepository.GetUserAsync(dto.UserName, dto.Password); // or check by username only
        if (existingUser != null)
            return null;

        var user = new User
        {
            UserName = dto.UserName,
            Password = dto.Password,
            RecordDate = DateTime.UtcNow
        };

        await _userRepository.AddUserAsync(user);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            RecordDate = user.RecordDate
        };
    }
}
