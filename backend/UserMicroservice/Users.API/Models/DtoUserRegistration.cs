using System.ComponentModel.DataAnnotations;
using Users.db.Entities;

namespace Users.API.Models
{
    public class DtoUserRegistration
    {
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [MaxLength(100)]
        public required string LastName { get; set; }
        [MaxLength(100)]
        public string? MiddleName { get; set; }
        [MaxLength(100)]
        public required string Email { get; set; }
        public required string Password { get; set; }
        [MaxLength(100)]
        public required string Phone { get; set; }
        public GenderTypes gender { get; set; }
        public required string Role { get; set; }
    }
}
