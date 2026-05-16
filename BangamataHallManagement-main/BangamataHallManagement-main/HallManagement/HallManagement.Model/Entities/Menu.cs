using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class Menu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public int? ParentMenuId { get; set; }

    public int? DisplayOrder { get; set; }

    public virtual ICollection<Menu> InverseParentMenu { get; set; } = new List<Menu>();

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();

    public virtual Menu? ParentMenu { get; set; }
}
