using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class StaffHistory
{
    public int LogId { get; set; }

    public int Id { get; set; }

    public string? Name { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? BioLink { get; set; }

    public DateTime? ActingDateFrom { get; set; }

    public DateTime? ActingDateTo { get; set; }

    public int? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public int? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public int? DisplayOrder { get; set; }

    public DateTime? LogDate { get; set; }

    public string? Action { get; set; }

    public virtual Staff IdNavigation { get; set; } = null!;
}
