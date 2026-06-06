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
    public class StudentVm
    {
        public StudentVm()
        {
            BatchList = BloodGroupList = ClassList = DepartmentList = StafList = NationalityList = ReligionList = SectionList = SessionList = new List<SelectListItem>();
        }
        public int? Id { get; set; }

        [DisplayName("Student Name(In English)")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between 3 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z.\s]+$", ErrorMessage = "{0} can only contain letters, dots, and spaces.")]
        public string NameInEnglish { get; set; }

        [DisplayName("Student Name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between 3 and 100 characters.")]
        //[RegularExpression(@"^[a-zA-Z.\s]+$", ErrorMessage = "{0} can only contain letters, dots, and spaces.")]
        public string Name { get; set; }

        [DisplayName("Father Name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between 3 and 100 characters.")]
        //[RegularExpression(@"^[a-zA-Z.\s]+$", ErrorMessage = "{0} can only contain letters, dots, and spaces.")]
        public string? FatherName { get; set; }

        [DisplayName("Mother Name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between 3 and 100 characters.")]
        //[RegularExpression(@"^[a-zA-Z.\s]+$", ErrorMessage = "{0} can only contain letters, dots, and spaces.")]
        public string? MotherName { get; set; }

        [DisplayName("Class Roll No.")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters.")]
        //[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "{0} can only contain alphanumeric characters.")]
        public string? ClassRollNo { get; set; }

        [DisplayName("Registration No.")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} must be between 1 and 50 characters.")]
        //[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "{0} can only contain alphanumeric characters.")]
        public string? RegistrationNo { get; set; }

        [DisplayName("Class")]
        public int? ClassId { get; set; }

        [DisplayName("Department")]
        public int? DepartmentId { get; set; }

        [DisplayName("Batch")]
        public int? BatchId { get; set; }

        [DisplayName("Section")]
        public int? SectionId { get; set; }

        [DisplayName("Session")]
        public int? SessionId { get; set; }

        [DisplayName("Registration Year")]
        //[RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter a 4-digit year.")]
        public string RegistrationYear { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [RegularExpression(@"^(013|014|015|016|017|018|019)\d{8}$", ErrorMessage = "Invalid mobile number.")]
        public string? Mobile { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [MaxLength(250, ErrorMessage = "Max length is 250 characters")]
        public string? Email { get; set; }

        [DisplayName("Religion")]
        public int? ReligionId { get; set; }

        [DisplayName("Nationality")]
        public int? NationalityId { get; set; }

        [DisplayName("Blood Group")]
        public int? BloodGroupId { get; set; }
        
        public bool? IsArchived { get; set; }

        public List<SelectListItem> BatchList { get; set; }

        public List<SelectListItem> BloodGroupList { get; set; }

        public List<SelectListItem> ClassList { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }

        public List<SelectListItem> StafList { get; set; }

        public List<SelectListItem> NationalityList { get; set; }

        public List<SelectListItem> ReligionList { get; set; }

        public List<SelectListItem> SectionList { get; set; }

        public List<SelectListItem> SessionList { get; set; }
    }
}
