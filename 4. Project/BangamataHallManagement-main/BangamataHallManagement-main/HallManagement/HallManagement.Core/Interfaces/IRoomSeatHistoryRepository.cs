using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Core.Interfaces
{
    public interface IRoomSeatHistoryRepository : IRepositoryBase<RoomSeatHistory>
    {
        IEnumerable<RoomSeatHistoryVm> GetRoomSeatHistoryBySeatId(int seatId);
        IEnumerable<RoomSeatHistoryVm> GetRoomSeatHistoryByStudentId(int studentId);
    }
}
