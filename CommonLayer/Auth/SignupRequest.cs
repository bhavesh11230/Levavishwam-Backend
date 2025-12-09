namespace Levavishwam_Backend.CommonLayer.Auth
{
    public class SignupRequest
    {
        public string Name { get; set; }          
        public string Email { get; set; }         
        public string Mobile { get; set; }        
        public string Address { get; set; }       

        public DateTime DOB { get; set; }         
        public string Gender { get; set; }        

        public string CommunityInfo { get; set; } 

        public string ProfilePhoto { get; set; }  
        public string Password { get; set; }
    }
    public class SignupResponse
    {
        public bool IsSuccess { get; set; }       
        public string Message { get; set; }       
        public int? UserId { get; set; }
    }
}
