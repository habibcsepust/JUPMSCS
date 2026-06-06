using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class RoomSeatHistoryVm
    {
        public string? SeatNo { get; set; }

        public string? RoomNo { get; set; }

        public string? StudentInfo { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
