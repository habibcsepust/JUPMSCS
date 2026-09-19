using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class RoomService : IRoomService
    {

        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Room GetById(int Id)
        {
            return _roomRepository.GetById(Id).Result;
        }

        public IEnumerable<Room> GetAll()
        {
            return _roomRepository.GetAll().Result;
        }

        public bool Create(Room room)
        {
           return  _roomRepository.Create(room);
        }

        public bool Update(Room room)
        {
           return  _roomRepository.Update(room);
        }

        public bool Delete(int id)
        {
            var room = _roomRepository.GetById(id)?.Result;
            if (room == null)
                return false;
            return _roomRepository.Delete(room);              
        }
    }
}
