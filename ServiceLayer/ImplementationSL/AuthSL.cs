using Levavishwam_Backend.CommonLayer.Auth;
using Levavishwam_Backend.RepositoryLayer;
using Levavishwam_Backend.Services;

namespace Levavishwam_Backend.ServiceLayer
{
    public class AuthSL : IAuthSL
    {
        private readonly IAuthRL _authRL;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthSL(IAuthRL authRL, JwtService jwtService, IConfiguration configuration)
        {
            _authRL = authRL;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
        {
            return await _authRL.SignupAsync(signupRequest);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            
            var repoResponse = await _authRL.LoginAsync(loginRequest);
            if (!repoResponse.IsSuccess)
                return repoResponse;

            
            var token = _jwtService.GenerateToken(repoResponse.UserId, loginRequest.Email);

            repoResponse.Token = token;
            return repoResponse;
        }
    }
}
