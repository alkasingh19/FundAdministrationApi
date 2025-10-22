using FundAdministrationApi.DTOs;

namespace FundAdministrationApi.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> AuthenticateAsync(LoginRequestDto login);
    }
}
