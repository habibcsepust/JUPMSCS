using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class Room
{
    public int Id { get; set; }

    public string RoomNo { get; set; } = null!;

    public int? StudentCapacity { get; set; }

    public virtual ICollection<RoomSeat> RoomSeats { get; set; } = new List<RoomSeat>();
}
