using Levavishwam_Backend.CommonLayer.Auth;

namespace Levavishwam_Backend.RepositoryLayer.InterfaceRL
{
    public interface IAuthRepository
    {
        public Task<SignupResponse> SignupAsync(SignupRequest signupRequest);
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
