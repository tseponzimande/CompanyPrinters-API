using System.ComponentModel.DataAnnotations;

namespace Exercise03ex01.Models
{
    public class User
    {
        public int id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? Token { get; set; }
        public string? role { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }       
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
