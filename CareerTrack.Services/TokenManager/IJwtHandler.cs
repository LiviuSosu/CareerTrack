
namespace CareerTrack.Services.TokenManager
{
    public interface IJwtHandler
    {
        JsonWebToken Create(JwtHandlerDTO jwtHandlerDTO);
    }
}
