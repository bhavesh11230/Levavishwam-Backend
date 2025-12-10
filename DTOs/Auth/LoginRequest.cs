namespace Levavishwam_Backend.CommonLayer.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; }     
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }   
        public string Message { get; set; }   
        public string Token { get; set; }     
        public int UserId { get; set; }       
        public string Name { get; set; }      
    }
}
