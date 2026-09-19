using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class Batch
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
