using DatingApp.Domain.Entities;

namespace DatingApp.BLL.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
