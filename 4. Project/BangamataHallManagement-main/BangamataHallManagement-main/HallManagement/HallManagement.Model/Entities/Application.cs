using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class Application
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public DateTime ApplicationDate { get; set; }

    public int? HallId { get; set; }

    public string? PreferredRoomType { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ApprovedDate { get; set; }

    public DateTime? RejectedDate { get; set; }

    public string? Remarks { get; set; }

    public int? ProcessedBy { get; set; }

    public DateTime? ProcessedDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Hall? Hall { get; set; }

    public virtual Student Student { get; set; } = null!;
}
