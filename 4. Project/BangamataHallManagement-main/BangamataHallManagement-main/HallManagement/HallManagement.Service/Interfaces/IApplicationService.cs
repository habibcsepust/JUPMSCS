using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IApplicationService
    {
        IEnumerable<Application> GetAll();

        Application GetById(int Id);

        bool Create(Application application);

        bool Update(Application application);

        bool Delete(int id);
    }
}