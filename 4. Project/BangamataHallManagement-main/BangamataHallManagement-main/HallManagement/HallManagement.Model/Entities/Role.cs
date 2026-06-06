using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();

    public virtual ICollection<UserCredential> UserCredentials { get; set; } = new List<UserCredential>();
}
