using Microsoft.AspNetCore.Identity;

namespace DietitianWebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public char Sex { get; set; }
        public string Nationality { get; set; }
        public string? CivilID { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public MedicalProfile MedicalProfile { get; set; }
    }
}
