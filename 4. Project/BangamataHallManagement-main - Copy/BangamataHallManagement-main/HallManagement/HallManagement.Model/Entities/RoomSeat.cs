using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class RoomSeat
{
    public int Id { get; set; }

    public string? SeatNo { get; set; }

    public int RoomId { get; set; }

    public int? StudentId { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<RoomSeatHistory> RoomSeatHistories { get; set; } = new List<RoomSeatHistory>();

    public virtual Student? Student { get; set; }

    public virtual Staff? UpdatedByNavigation { get; set; }
}
