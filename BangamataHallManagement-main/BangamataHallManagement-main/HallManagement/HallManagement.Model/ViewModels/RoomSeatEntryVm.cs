using HallManagement.Model.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class RoomSeatEntryVm
    {
        public RoomSeatEntryVm()
        {
            StudentsDdl = RoomSeatsDdl = new List<SelectListItem>();
        }

        [DisplayName("Room")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Id { get; set; }

        public int RoomId { get; set; }

        public string? SeatNo { get; set; }

        public bool? IsAllocated { get; set; }

        public bool? IsUsable { get; set; }

        [DisplayName("Student")]
        //[Required(ErrorMessage ="The field {0} is required")]
        public int? StudentId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }
        public List<SelectListItem> StudentsDdl { get; set; }
        public List<SelectListItem> RoomSeatsDdl { get; set; }
    }
}
