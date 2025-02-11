using System;
using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models
{
    public class LoginLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool IsSuccessful { get; set; }  // ✅ True = login reușit, False = eșuat

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;  // ✅ Momentul autentificării
    }
}
