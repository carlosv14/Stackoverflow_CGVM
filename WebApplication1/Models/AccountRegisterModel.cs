using System.ComponentModel.DataAnnotations;
using WebApplication1.Controllers.CostumeDataNotation;

namespace WebApplication1.Controllers
{
    public class AccountRegisterModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Min characters: 8, Max characters: 16", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Min characters: 8, Max characters: 16", MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(16,ErrorMessage = "Min characters: 8, Max characters: 16",MinimumLength = 8)]
        [RegularExpression("[a-zA-Z0-9]*", ErrorMessage = "Only numbers and letters")]
        [VocalConfirmation(ErrorMessage= "Must contain at least a vocal")]
        [ContainNumber(ErrorMessage = "Must contain at least a number")]
        [NoRepeatingLetter(ErrorMessage = "can't repeat the same letter twice in a row")]
        [Password(ErrorMessage = "Must contain at least one capital")]
        [DataType(DataType.Password)]
        public string Passw { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

       
    }
}