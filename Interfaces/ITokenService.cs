using Tour_API.Models;

namespace Tour_API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user, string userRole);
    }
}
