using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Password { get; set; }

        [DisplayName("Login As")]
        [Required(ErrorMessage = "The field {0} is required")]
        public bool IsStudentLogin { get; set; }
    }
}
