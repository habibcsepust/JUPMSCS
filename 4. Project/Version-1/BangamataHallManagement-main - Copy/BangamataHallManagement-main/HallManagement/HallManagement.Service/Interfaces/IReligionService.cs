using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IReligionService
    {
        IEnumerable<Religion> GetAll();
        Religion GetById(int Id);
        bool Create(Religion religion);
        bool Update(Religion religion);
        bool Delete(int id);
    }
}
