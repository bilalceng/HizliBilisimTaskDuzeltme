using HizliBilisim.models;

namespace HizliBilisim.services.interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}