using Levavishwam_Backend.CommonLayer.Auth;

namespace Levavishwam_Backend.ServiceLayer.InterfaceSL
{
    public interface IAuthSL
    {
        public Task<SignupResponse> SignupAsync(SignupRequest signupRequest);
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
