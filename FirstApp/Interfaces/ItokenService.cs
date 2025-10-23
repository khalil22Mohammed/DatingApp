using FirstApp.Entities;

namespace FirstApp.Interfaces
{
    public interface ItokenService
    {
        string CreateToken(AppUser user);
    }
}
