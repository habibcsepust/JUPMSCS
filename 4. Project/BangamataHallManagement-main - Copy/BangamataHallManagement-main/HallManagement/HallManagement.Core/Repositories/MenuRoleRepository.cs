using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Repositories.GenericRepository;

namespace HallManagement.Core.Repositories
{
    public class MenuRoleRepository : RepositoryBase<MenuRole>, IMenuRoleRepository
    {
        BangamataHallContext _applicationContext;
        public MenuRoleRepository(BangamataHallContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public List<MenuVm> GetMenuItems(int roleId)
        {
            var menuItems = _applicationContext.MenuRoles.Where(x => x.RoleId == roleId).Select(x => x.Menu);
            var menuVmList = new List<MenuVm>();

            foreach (var mainMenu in menuItems.Where(x => x.ParentMenuId == null).OrderBy(x => x.DisplayOrder))
            {
                menuVmList.Add(new MenuVm
                {
                    Id = mainMenu.Id,
                    Name = mainMenu.Name,
                    Url = mainMenu.Url,
                    DisplayOrder = mainMenu.DisplayOrder,
                    ParentMenuId = mainMenu.ParentMenuId,
                    SubMenus = new List<MenuVm>()
                });
            }
            foreach (var menuVm in menuVmList)
            {
                var subMenus = menuItems.Where(x => x.ParentMenuId == menuVm.Id).OrderBy(x => x.DisplayOrder).Select(x => new MenuVm
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url,
                    DisplayOrder = x.DisplayOrder,
                    ParentMenuId = x.ParentMenuId
                }).ToList();
                menuVm.SubMenus = subMenus;
            }
            return menuVmList;
        }

        //public Department? GetByEmailOrPhone(string emailOrPhone)
        //{
        //    return _applicationContext.Departments.Where(x => (x.Email == emailOrPhone || x.MobileNo == emailOrPhone) && x.StatusId == (int)RemittanceTypeEnum.SentToMaker).OrderByDescending(x => x.CreateDate).FirstOrDefault();
        //}
    }
}
