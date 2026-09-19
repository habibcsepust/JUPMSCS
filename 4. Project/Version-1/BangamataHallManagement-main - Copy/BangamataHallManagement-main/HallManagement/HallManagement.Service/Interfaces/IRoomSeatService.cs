using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;

namespace HallManagement.Service.Interfaces
{
    public interface IRoomSeatService
    {
        IEnumerable<RoomSeat> GetAll();
        RoomSeat GetById(int Id);
        bool Create(RoomSeat roomSeat);
        bool Update(RoomSeat roomSeat);
        bool Delete(int id);
        IEnumerable<RoomSeatVm> GetRoomSeatsByStudentId(int studentId);
    }
}
