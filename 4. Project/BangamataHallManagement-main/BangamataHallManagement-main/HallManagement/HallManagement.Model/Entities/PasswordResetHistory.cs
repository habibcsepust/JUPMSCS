using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class PasswordResetHistory
{
    public int Id { get; set; }

    public string? HashedPasswordResetLink { get; set; }

    public DateTime? ExpiryDateTime { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UserId { get; set; }

    public bool? IsStudent { get; set; }
}
