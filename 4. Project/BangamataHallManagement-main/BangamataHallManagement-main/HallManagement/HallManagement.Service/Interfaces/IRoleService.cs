using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(int Id);
        bool Create(Role role);
        bool Update(Role role);
        bool Delete(int id);
    }
}
