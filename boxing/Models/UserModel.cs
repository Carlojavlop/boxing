using System.ComponentModel.DataAnnotations;

namespace boxing.Models
{
    public class UserModel
    {

        public int id_user { get; set; }
        [Required]
        public string? names { get; set; }
        [Required]
        public string? lastname { get; set; }
        [Required]
        public DateTime birthdate { get; set; }
        [Required]
        public string? mail { get; set; }
        [Required]
        public string? users { get; set; }
        [Required]
        public string? password { get; set; }
        [Required]
        public string? token { get; set; }
        public byte states { get; set; }
    }
}
