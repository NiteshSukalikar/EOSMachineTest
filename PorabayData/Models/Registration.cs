using System.ComponentModel.DataAnnotations;

namespace PorabayData.Models
{
    public class Registration
    {
        [Key]
        public int RegisterID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
