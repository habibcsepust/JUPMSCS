using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class Hall
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
