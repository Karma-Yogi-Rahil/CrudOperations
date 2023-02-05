using CrudOperations.Encryption;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudOperations.Models
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string EmailId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} character long", MinimumLength = 6)]
        [DisplayName("Password")]
        public string EncryptedPassword { get; set; }

        public string SaltPassword { get; set; } = PasswordEncryption.GenrateSaltCode();

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;


    }
}
