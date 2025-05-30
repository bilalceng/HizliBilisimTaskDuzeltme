using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HizliBilisim.Controllers
{
    public class BaseController : ControllerBase
    {
 
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("userId");
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token.");

            return int.Parse(userIdClaim.Value);
        }
    }
}