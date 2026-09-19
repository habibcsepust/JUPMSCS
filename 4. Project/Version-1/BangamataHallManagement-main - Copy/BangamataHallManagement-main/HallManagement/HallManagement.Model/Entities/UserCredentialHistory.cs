using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class UserCredentialHistory
{
    public int LogId { get; set; }

    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public int StaffId { get; set; }

    public int RoleId { get; set; }

    public int? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public int? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool IsEnabled { get; set; }

    public DateTime? LogDate { get; set; }

    public string? Action { get; set; }

    public virtual UserCredential IdNavigation { get; set; } = null!;
}
