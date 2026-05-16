using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IClassService
    {
        IEnumerable<Class> GetAll();
        Class GetById(int Id);
        bool Create(Class clas);
        bool Update(Class clas);
        bool Delete(int id);
    }
}
