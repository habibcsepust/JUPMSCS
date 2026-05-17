using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class RoomSeatHistory
{
    public int LogId { get; set; }

    public int Id { get; set; }

    public string? SeatNo { get; set; }

    public int RoomId { get; set; }

    public int? StudentId { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? LogDate { get; set; }

    public string? Action { get; set; }

    public virtual RoomSeat IdNavigation { get; set; } = null!;
}
