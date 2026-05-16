using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;

namespace HallManagement.Service.Interfaces
{
    public interface IMenuRoleService
    {
        IEnumerable<MenuRole> GetAll();
        MenuRole GetById(int Id);
        bool Create(MenuRole menuRole);
        bool Update(MenuRole menuRole);
        bool Delete(int id);
        List<MenuVm> GetMenuItems(int roleId);
    }
}
