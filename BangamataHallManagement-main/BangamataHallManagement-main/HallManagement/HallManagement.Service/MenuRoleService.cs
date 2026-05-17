using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class MenuRoleService : IMenuRoleService
    {

        private readonly IMenuRoleRepository _menuRoleRepository;

        public MenuRoleService(IMenuRoleRepository menuRoleRepository)
        {
            _menuRoleRepository = menuRoleRepository;
        }

        public MenuRole GetById(int Id)
        {
            return _menuRoleRepository.GetById(Id).Result;
        }

        public IEnumerable<MenuRole> GetAll()
        {
            return _menuRoleRepository.GetAll().Result;
        }

        public bool Create(MenuRole menuRole)
        {
           return  _menuRoleRepository.Create(menuRole);
        }

        public bool Update(MenuRole menuRole)
        {
           return  _menuRoleRepository.Update(menuRole);
        }

        public bool Delete(int id)
        {
            var menuRole = _menuRoleRepository.GetById(id)?.Result;
            if (menuRole == null)
                return false;
            return _menuRoleRepository.Delete(menuRole);              
        }

        public List<MenuVm> GetMenuItems(int roleId)
        {
            return _menuRoleRepository.GetMenuItems(roleId);
        }
    }
}
