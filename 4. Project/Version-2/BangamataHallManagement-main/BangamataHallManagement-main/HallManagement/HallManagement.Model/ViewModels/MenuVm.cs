using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Model.ViewModels
{
    public class MenuVm
    {
        public MenuVm()
        {
            SubMenus = new List<MenuVm>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public int? ParentMenuId { get; set; }
        public int? DisplayOrder { get; set; }
        public List<MenuVm> SubMenus { get; set; }
    }
}
