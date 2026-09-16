using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class RoomSeatService : IRoomSeatService
    {

        private readonly IRoomSeatRepository _roomSeatRepository;

        public RoomSeatService(IRoomSeatRepository roomSeatRepository)
        {
            _roomSeatRepository = roomSeatRepository;
        }

        public RoomSeat GetById(int Id)
        {
            return _roomSeatRepository.GetById(Id).Result;
        }

        public IEnumerable<RoomSeat> GetAll()
        {
            return _roomSeatRepository.GetAll().Result;
        }

        public bool Create(RoomSeat roomSeat)
        {
           return  _roomSeatRepository.Create(roomSeat);
        }

        public bool Update(RoomSeat roomSeat)
        {
           return  _roomSeatRepository.Update(roomSeat);
        }

        public bool Delete(int id)
        {
            var roomSeat = _roomSeatRepository.GetById(id)?.Result;
            if (roomSeat == null)
                return false;
            return _roomSeatRepository.Delete(roomSeat);              
        }

        public IEnumerable<RoomSeatVm> GetRoomSeatsByStudentId(int studentId)
        {
            return _roomSeatRepository.GetRoomSeatsByStudentId(studentId);
        }
    }
}
