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
    public class StudentDetailVm
    {
        public int? Id { get; set; }
        [DisplayName("Student Name(In English)")]
        [ReadOnly(true)]
        public string NameInEnglish { get; set; }

        [DisplayName("Student Name")]
        [ReadOnly(true)]
        public string Name { get; set; }

        [DisplayName("Father Name")]
        public string? FatherName { get; set; }

        [DisplayName("Mother Name")]
        public string? MotherName { get; set; }

        [DisplayName("Class Roll No.")]
        public string? ClassRollNo { get; set; }

        [DisplayName("Registration No.")]
        public string? RegistrationNo { get; set; }

        [DisplayName("Class")]
        public string? ClassName { get; set; }

        [DisplayName("Department")]
        public string? DepartmentName { get; set; }

        [DisplayName("Batch")]
        public string? BatchName { get; set; }

        [DisplayName("Section")]
        public string? SectionName { get; set; }

        [DisplayName("Session")]
        public string? SessionName { get; set; }

        [DisplayName("Registration Year")]
        public string? RegistrationYear { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        [DisplayName("Religion")]
        public string? ReligionName { get; set; }

        [DisplayName("Nationality")]
        public string? NationalityName { get; set; }

        [DisplayName("Blood Group")]
        public string? BloodGroupName { get; set; }

        [DisplayName("Archived Already?")]
        public string? IsArchived { get; set; }

        [DisplayName("Entry Date")]
        public DateTime? EntryDate { get; set; }

        [DisplayName("Entry By")]
        public string? EntryBy { get; set; }

        [DisplayName("Last Update Date")]
        public DateTime? ModifyDate { get; set; }

        [DisplayName("Last Update By")]
        public string? ModifiedBy { get; set; }
    }
}
