using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class RoomSeatHistoryService : IRoomSeatHistoryService
    {

        private readonly IRoomSeatHistoryRepository _roomSeatHistoryRepository;

        public RoomSeatHistoryService(IRoomSeatHistoryRepository roomSeatHistoryRepository)
        {
            _roomSeatHistoryRepository = roomSeatHistoryRepository;
        }

        public RoomSeatHistory GetById(int Id)
        {
            return _roomSeatHistoryRepository.GetById(Id).Result;
        }

        public IEnumerable<RoomSeatHistory> GetAll()
        {
            return _roomSeatHistoryRepository.GetAll().Result;
        }

        public bool Create(RoomSeatHistory roomSeatHistory)
        {
            return _roomSeatHistoryRepository.Create(roomSeatHistory);
        }

        public bool Update(RoomSeatHistory roomSeatHistory)
        {
            return _roomSeatHistoryRepository.Update(roomSeatHistory);
        }

        public bool Delete(int id)
        {
            var roomSeatHistory = _roomSeatHistoryRepository.GetById(id)?.Result;
            if (roomSeatHistory == null)
                return false;
            return _roomSeatHistoryRepository.Delete(roomSeatHistory);
        }

        public IEnumerable<RoomSeatHistoryVm> GetRoomSeatHistoryBySeatId(int seatId)
        {
            return _roomSeatHistoryRepository.GetRoomSeatHistoryBySeatId(seatId);
        }

        public IEnumerable<RoomSeatHistoryVm> GetRoomSeatHistoryByStudentId(int studentId)
        {
            return _roomSeatHistoryRepository.GetRoomSeatHistoryByStudentId(studentId);
        }
    }
}
