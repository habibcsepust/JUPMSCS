using HallManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HallManagement.Model.ViewModels
{
    public class StaffVm
    {
        public StaffVm()
        {
            DepartmentList = DesignationList = new List<SelectListItem>();
        }
        public int Id { get; set; }

        [DisplayName("Staff Name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between 3 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z.\s]+$", ErrorMessage = "{0} can only contain letters, dots, and spaces.")]
        public string Name { get; set; }

        [DisplayName("Department")]
        public int? DepartmentId { get; set; }

        [DisplayName("Designation")]
        public int? DesignationId { get; set; }

        [DisplayName("Bio Link")]
        [MaxLength(250, ErrorMessage = "Max length is 250 characters")]
        public string? BioLink { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"^(013|014|015|016|017|018|019)\d{8}$", ErrorMessage = "Invalid mobile number.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [MaxLength(250, ErrorMessage = "Max length is 250 characters")]
        public string Email { get; set; }

        [DisplayName("Acting Date(from)")]
        public DateTime? ActingDateFrom { get; set; }

        [DisplayName("Acting Date(to)")]
        public DateTime? ActingDateTo { get; set; }

        [DisplayName("Entry By")]
        public int? EntryBy { get; set; }

        [DisplayName("Entry Date")]
        public DateTime? EntryDate { get; set; }

        [DisplayName("Modify By")]
        public int? ModifyBy { get; set; }

        [DisplayName("Modify Date")]
        public DateTime? ModifyDate { get; set; }

        [DisplayName("Display Order")]
        [Required(ErrorMessage = "{0} is required.")]
        public int? DisplayOrder { get; set; }

        [DisplayName("Active Staff?")]
        [Required(ErrorMessage = "{0} is required.")]
        public bool? IsActive { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }

        public List<SelectListItem> DesignationList { get; set; }
    }
}
