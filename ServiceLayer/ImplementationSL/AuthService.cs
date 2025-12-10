using Levavishwam_Backend.CommonLayer.Auth;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Levavishwam_Backend.Services;

namespace Levavishwam_Backend.ServiceLayer.ImplementationSL
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly JwtTokenGenerator _jwt;

        public AuthService(IAuthRepository repository, JwtTokenGenerator jwt)
        {
            _repository = repository;
            _jwt = jwt;
        }

        public async Task<SignupResponse> SignupAsync(SignupRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return new SignupResponse
                {
                    IsSuccess = false,
                    Message = "Password & Confirm Password do not match"
                };
            }

            return await _repository.SignupAsync(request);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = await _repository.LoginAsync(request);

            if (!response.IsSuccess)
                return response;

            response.Token = _jwt.GenerateToken(response.UserId, request.Email);
            return response;
        }
    }
}
