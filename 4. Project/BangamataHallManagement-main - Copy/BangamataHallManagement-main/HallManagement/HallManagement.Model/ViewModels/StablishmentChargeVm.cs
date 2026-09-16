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
    public class StablishmentChargeVm
    {
        public StablishmentChargeVm()
        {
            StudentList = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [DisplayName("Student")]
        [Required(ErrorMessage = "{0} is required.")]
        public int? StudentId { get; set; }

        [DisplayName("Charge for Year")]
        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Please enter a 4-digit year.")]
        public int? Year { get; set; }

        [DisplayName("Paid Amount")]
        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid {0}, Maximum Two Decimal Points.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid {0}.")]
        public decimal? PaidAmount { get; set; }

        public List<SelectListItem> StudentList { get; set; }
    }
}
