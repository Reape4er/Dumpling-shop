using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.db.Entities
{
    /// <summary>
    /// таблица пользователя
    /// </summary>
    public class DbUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Phone { get; set; }
        public GenderTypes gender { get; set; }
        public string Role { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? Deleted { get; set; } = null;
        
    }

    public enum GenderTypes : byte {
    Default=2,
    Male=1,
    Female=0
    }
}
