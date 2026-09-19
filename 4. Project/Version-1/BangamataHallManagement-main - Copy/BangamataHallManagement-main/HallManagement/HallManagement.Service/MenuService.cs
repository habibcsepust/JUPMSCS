using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Service.Interfaces;

namespace HallManagement.Service
{
    public class MenuService : IMenuService
    {

        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public Menu GetById(int Id)
        {
            return _menuRepository.GetById(Id).Result;
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuRepository.GetAll().Result;
        }

        public bool Create(Menu menu)
        {
           return  _menuRepository.Create(menu);
        }

        public bool Update(Menu menu)
        {
           return  _menuRepository.Update(menu);
        }

        public bool Delete(int id)
        {
            var menu = _menuRepository.GetById(id)?.Result;
            if (menu == null)
                return false;
            return _menuRepository.Delete(menu);              
        }
    }
}
