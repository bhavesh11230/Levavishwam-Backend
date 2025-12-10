using Levavishwam_Backend.CommonLayer.Auth;

namespace Levavishwam_Backend.ServiceLayer.InterfaceSL
{
    public interface IAuthService
    {
        public Task<SignupResponse> SignupAsync(SignupRequest signupRequest);
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
