using System.ComponentModel.DataAnnotations;

namespace HealthPadiWebApi.DTOs.Response
{
    public class RegisterResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
