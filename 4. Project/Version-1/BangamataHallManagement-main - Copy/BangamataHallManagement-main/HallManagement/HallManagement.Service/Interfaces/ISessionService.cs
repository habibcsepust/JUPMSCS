using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<Session> GetAll();
        Session GetById(int Id);
        bool Create(Session session);
        bool Update(Session session);
        bool Delete(int id);
    }
}
