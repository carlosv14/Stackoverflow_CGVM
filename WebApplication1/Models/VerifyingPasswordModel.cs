using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Controllers.CostumeDataNotation;

namespace WebApplication1.Models
{
    public class VerifyingPasswordModel
    {
        [Required]
        [StringLength(16, ErrorMessage = "Min characters: 8, Max characters: 16", MinimumLength = 8)]
        [RegularExpression("[a-zA-Z0-9]*", ErrorMessage = "Only numbers and letters")]
        [VocalConfirmation(ErrorMessage = "Must contain at least a vocal")]
        [ContainNumber(ErrorMessage = "Must contain at least a number")]
        [NoRepeatingLetter(ErrorMessage = "can't repeat the same letter twice in a row")]
        [Password(ErrorMessage = "Must contain at least one capital")]
        [DataType(DataType.Password)]
        public string Passw { get; set; }
         [Required]
        [DataType(DataType.Password)]
        public string confirmPassw{ get; set; }
        
    }
}