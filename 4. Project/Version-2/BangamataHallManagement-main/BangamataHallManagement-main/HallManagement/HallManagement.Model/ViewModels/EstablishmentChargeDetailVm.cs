using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class EstablishmentChargeDetailVm
    {
        public int Id { get; set; }

        [DisplayName("Class Roll No.")]
        public string? ClassRollNo { get; set; }

        [DisplayName("Charge for Year")]
        public int? Year { get; set; }

        [DisplayName("Paid Amount")]
        public decimal? PaidAmount { get; set; }

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
