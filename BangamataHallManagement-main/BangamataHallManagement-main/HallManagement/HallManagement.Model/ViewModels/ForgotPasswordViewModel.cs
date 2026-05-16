using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [DisplayName("Mobile Number")]
        [RegularExpression(@"^(013|014|015|016|017|018|019)\d{8}$", ErrorMessage = "Invalid mobile number.")]
        public string Mobile { get; set; }

        [DisplayName("User Type")]
        [Required(ErrorMessage = "The field {0} is required")]
        public bool IsStudentLogin { get; set; }
    }
}
