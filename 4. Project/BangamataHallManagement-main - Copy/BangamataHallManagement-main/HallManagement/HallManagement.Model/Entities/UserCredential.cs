using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class UserCredential
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public int StaffId { get; set; }

    public int RoleId { get; set; }

    public int? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public int? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool IsEnabled { get; set; }

    public bool? IsPasswordResetDone { get; set; }

    public virtual Staff? EntryByNavigation { get; set; }

    public virtual Staff? ModifyByNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;

    public virtual ICollection<UserCredentialHistory> UserCredentialHistories { get; set; } = new List<UserCredentialHistory>();
}
