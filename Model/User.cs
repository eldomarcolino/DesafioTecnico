using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SistemaDeRecarga.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdCourse")]
        public int IdCourse { get; set; }

        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        [StringLength(20)]
        public string RegistrationNumber { get; set; }

        public string Role { get; set; }

        public DateTime Createdate { get; set; }

        [JsonIgnore]
        [ForeignKey("IdCourse")]
        public Curso? IdCourseNavigation { get; set; }

        [JsonIgnore]
        public Balance? Balance { get; set; }

        [JsonIgnore]
        public ICollection<Transacao> Transaction { get; set; } = new List<Transacao>();
    }

    public class UserDTO
    {
        public int Id { get; set; }
        public int IdCourse { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RegistrationNumber { get; set; }
        public string Role { get; set; }
        public DateTime Createdate { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
