namespace HealthPadiWebApi.DTOs.Response
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public UserDto User { get; set; }
    }
}
