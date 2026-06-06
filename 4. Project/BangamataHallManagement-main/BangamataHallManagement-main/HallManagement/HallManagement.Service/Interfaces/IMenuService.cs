using HallManagement.Model.Entities;

namespace HallManagement.Service.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<Menu> GetAll();
        Menu GetById(int Id);
        bool Create(Menu menu);
        bool Update(Menu menu);
        bool Delete(int id);
    }
}
