using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface INationalityService
    {
        IEnumerable<Nationality> GetAll();
        Nationality GetById(int Id);
        bool Create(Nationality nationality);
        bool Update(Nationality nationality);
        bool Delete(int id);
    }
}
