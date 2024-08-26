using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateJWTToken(User user, List<String> roles);
    }
}