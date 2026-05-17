using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public bool IsStudentLogin { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "{0} must be between 8 and 20 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$", ErrorMessage = "Invalid password. Password must start with an uppercase letter, containing at least one special character, and have a mix of lowercase letters and numbers")]
        public string NewPassword { get; set; } = null!;

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "{0} must be between 8 and 20 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$", ErrorMessage = "Invalid password. Password must start with an uppercase letter, containing at least one special character, and have a mix of lowercase letters and numbers")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
