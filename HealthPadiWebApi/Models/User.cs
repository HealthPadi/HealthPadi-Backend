using Microsoft.AspNetCore.Identity;

namespace HealthPadiWebApi.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsDisabled { get; set; } = false;
        public double Point { get; set; } = 0;
        //Navigation Properties
        public ICollection<Report> Reports { get; set; } = new List<Report>();

    }
}
