using HallManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HallManagement.Model.ViewModels
{
    public class UserCredentialVm
    {
        public UserCredentialVm()
        {
            Staffs = Roles = new List<SelectListItem>();
        }

        public int Id { get; set; }

        //[DisplayName("User Name[Login ID]")]
        //[Required(ErrorMessage = "{0} is required.")]
        //[StringLength(20, MinimumLength = 8, ErrorMessage = "{0} must be between 8 and 20 characters.")]
        //public string UserName { get; set; } = null!;

        //[Required(ErrorMessage = "{0} is required.")]
        //[StringLength(20, MinimumLength = 8, ErrorMessage = "{0} must be between 8 and 20 characters.")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$", ErrorMessage = "Invalid password. Password must start with an uppercase letter, containing at least one special character, and have a mix of lowercase letters and numbers")]
        //public string Password { get; set; } = null!;

        [DisplayName("Staff Name")]
        [Required(ErrorMessage = "{0} is required.")]
        public int StaffId { get; set; }

        [DisplayName("User Role")]
        [Required(ErrorMessage = "{0} is required.")]
        public int RoleId { get; set; }

        public int? EntryBy { get; set; }
        [DisplayName("Entry By")]
        public string? EntryByStaffName { get; set; }

        [DisplayName("Entry Date")]
        public DateTime? EntryDate { get; set; }

        public int? ModifyBy { get; set; }
        [DisplayName("Modify by")]
        public string? ModifyByStaffName { get; set; }

        [DisplayName("Modify Date")]
        public DateTime? ModifyDate { get; set; }

        [DisplayName("User Role")]
        public string? RoleName { get; set; }

        [DisplayName("Staff Name")]
        public string? StaffName { get; set; }

        [DisplayName("Activate Password?")]
        [Required(ErrorMessage = "{0} is required.")]
        public bool? IsEnabled { get; set; }
        public List<SelectListItem> Staffs { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
