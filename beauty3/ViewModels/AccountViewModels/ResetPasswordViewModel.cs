using System.ComponentModel.DataAnnotations;

namespace beauty3.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Құпиясөз 6 символдан кем болмауы тиіс!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Құпиясөздер сәйкес емес!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
