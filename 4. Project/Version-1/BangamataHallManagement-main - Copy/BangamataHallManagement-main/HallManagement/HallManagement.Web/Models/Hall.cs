using System;
using System.Collections.Generic;

namespace HallManagement.Web.Models;

public partial class Hall
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
