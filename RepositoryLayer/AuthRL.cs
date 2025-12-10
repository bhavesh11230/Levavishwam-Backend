using Levavishwam_Backend.CommonLayer.Auth;
using BCrypt.Net;
using System.Data;
using System.Data.SqlClient;
namespace Levavishwam_Backend.RepositoryLayer
{
    public class AuthRL : IAuthRL
    {
        public readonly IConfiguration _configuration;
        public readonly string _connectionString;

        public AuthRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DBSettingConnection");
        }

        public async Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
        {
            var response = new SignupResponse();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    const string emailExistsQuery = "SELECT COUNT(1) FROM Users WHERE Email = @Email";
                    using (var cmd = new SqlCommand(emailExistsQuery, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", signupRequest.Email ?? string.Empty);

                        await con.OpenAsync();
                        var obj = await cmd.ExecuteScalarAsync();
                        int emailExists = Convert.ToInt32(obj ?? 0);

                        if (emailExists > 0)
                        {
                            return new SignupResponse
                            {
                                IsSuccess = false,
                                Message = "Email already exists"
                            };
                        }
                    }
                }

                // 2) Insert new user (hash password before storing)
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password);

                using (var con = new SqlConnection(_connectionString))
                {
                    const string insertQuery = @"
                        INSERT INTO Users
        (Name, Email, PasswordHash, Role, Status, CreatedAt)
    VALUES
        (@Name, @Email, @PasswordHash, 'User', 'Pending', GETDATE());
    SELECT SCOPE_IDENTITY();";

                    using (var cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", signupRequest.Name ?? string.Empty);
                        cmd.Parameters.AddWithValue("@Email", signupRequest.Email ?? string.Empty);
                        cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                        await con.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();
                        int newUserId = Convert.ToInt32(result);

                        response.IsSuccess = true;
                        response.Message = "Signup successful";
                        response.UserId = newUserId;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message; 
            }

            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var response = new LoginResponse();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    const string loginQuery = @"SELECT UserId, Name, Email, PasswordHash FROM Users WHERE Email = @Email";
                    using (var cmd = new SqlCommand(loginQuery, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", loginRequest.Email ?? string.Empty);

                        await con.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                        {
                            if (!await reader.ReadAsync())
                            {
                                response.IsSuccess = false;
                                response.Message = "Email not found";
                                return response;
                            }

                            int userId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0;
                            string name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty;
                            string passwordHash = reader["PasswordHash"] != DBNull.Value ? reader["PasswordHash"].ToString() : string.Empty;

                            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, passwordHash);
                            if (!isPasswordValid)
                            {
                                response.IsSuccess = false;
                                response.Message = "Invalid password";
                                return response;
                            }

                            response.IsSuccess = true;
                            response.Message = "Login successful";
                            response.UserId = userId;
                            response.Name = name;
                        }
                    }
                }
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
