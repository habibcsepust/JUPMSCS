using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class StaffDetailVm
    {
        public int Id { get; set; }

        [DisplayName("Staff Name")]
        public string Name { get; set; }

        [DisplayName("Department")]
        public string DepartmentName { get; set; }

        [DisplayName("Designation")]
        public string DesignationName { get; set; }

        [DisplayName("Bio Link")]
        public string? BioLink { get; set; }

        public string? Email { get; set; }

        public string? Mobile { get; set; }

        [DisplayName("Acting Date(from)")]
        public DateTime? ActingDateFrom { get; set; }

        [DisplayName("Acting Date(to)")]
        public DateTime? ActingDateTo { get; set; }

        [DisplayName("Entry By")]
        public string? EntryBy { get; set; }

        [DisplayName("Entry Date")]
        public DateTime? EntryDate { get; set; }

        [DisplayName("Modify By")]
        public string? ModifyBy { get; set; }

        [DisplayName("Modify Date")]
        public DateTime? ModifyDate { get; set; }

        [DisplayName("Display Order")]
        public int? DisplayOrder { get; set; }

        [DisplayName("Active Staff?")]
        public string IsActive { get; set; }
    }
}
