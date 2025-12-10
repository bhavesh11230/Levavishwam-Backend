using Levavishwam_Backend.CommonLayer.Auth;

namespace Levavishwam_Backend.RepositoryLayer
{
    public interface IAuthRL
    {
        public Task<SignupResponse> SignupAsync(SignupRequest signupRequest);
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
