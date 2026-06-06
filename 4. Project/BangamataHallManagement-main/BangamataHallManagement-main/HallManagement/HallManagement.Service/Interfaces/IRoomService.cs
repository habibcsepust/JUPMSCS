using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAll();
        Room GetById(int Id);
        bool Create(Room room);
        bool Update(Room room);
        bool Delete(int id);
    }
}
