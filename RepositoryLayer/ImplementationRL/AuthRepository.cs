using BCrypt.Net;
using Levavishwam_Backend.CommonLayer.Auth;
using Levavishwam_Backend.Data;
using Levavishwam_Backend.Models;
using Levavishwam_Backend.RepositoryLayer.InterfaceRL;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Levavishwam_Backend.RepositoryLayer.ImplementationRL
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _db;

        public AuthRepository(AppDbContext db)
        {
            _db = db;
        }

        // -------------------------------------------------------
        // SIGNUP
        // -------------------------------------------------------
        public async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
        {
            var response = new SignupResponse();

            try
            {
                // Check if email already exists
                bool exists = await _db.Users
                    .AnyAsync(u => u.Email == signupRequest.Email);

                if (exists)
                {
                    response.IsSuccess = false;
                    response.Message = "Email already exists";
                    return response;
                }

                // Hash password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password);

                // Create User entity
                var newUser = new User
                {
                    Name = signupRequest.Name,
                    Email = signupRequest.Email,
                    PasswordHash = hashedPassword,
                    Role = "User",
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                };

                _db.Users.Add(newUser);
                await _db.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Signup successful";
                response.UserId = newUser.UserId;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        // -------------------------------------------------------
        // LOGIN
        // -------------------------------------------------------
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var response = new LoginResponse();

            try
            {
                var user = await _db.Users
                    .FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Email not found";
                    return response;
                }

                // Validate password using BCrypt
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);

                if (!isValidPassword)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid password";
                    return response;
                }

                response.IsSuccess = true;
                response.Message = "Login successful";
                response.UserId = user.UserId;
                response.Name = user.Name;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
