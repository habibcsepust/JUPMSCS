using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class EstablishmentCharge
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? Year { get; set; }

    public decimal? PaidAmount { get; set; }

    public int? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public int? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public virtual Staff? EntryByNavigation { get; set; }

    public virtual Staff? ModifyByNavigation { get; set; }

    public virtual Student? Student { get; set; }
}
