using Microsoft.AspNetCore.Identity;

namespace HealthPadiWebApi.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        //Navigation Properties
        public ICollection<Report> Reports { get; set; } = new List<Report>();

    }
}
/*  public Guid UserId { get; set; }
        public string Email { get; set; }*/
//  public Role Role { get; set; }