using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;

namespace HallManagement.Service.Interfaces
{
    public interface IRoomSeatHistoryService
    {
        IEnumerable<RoomSeatHistory> GetAll();
        RoomSeatHistory GetById(int Id);
        bool Create(RoomSeatHistory roomSeat);
        bool Update(RoomSeatHistory roomSeat);
        bool Delete(int id);
        IEnumerable<RoomSeatHistoryVm> GetRoomSeatHistoryBySeatId(int seatId);
        IEnumerable<RoomSeatHistoryVm> GetRoomSeatHistoryByStudentId(int studentId);
    }
}
