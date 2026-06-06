using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class Staff
{
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

    public bool? IsActive { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Designation? Designation { get; set; }

    public virtual Staff? EntryByNavigation { get; set; }

    public virtual ICollection<EstablishmentCharge> EstablishmentChargeEntryByNavigations { get; set; } = new List<EstablishmentCharge>();

    public virtual ICollection<EstablishmentCharge> EstablishmentChargeModifyByNavigations { get; set; } = new List<EstablishmentCharge>();

    public virtual ICollection<Staff> InverseEntryByNavigation { get; set; } = new List<Staff>();

    public virtual ICollection<Staff> InverseModifyByNavigation { get; set; } = new List<Staff>();

    public virtual Staff? ModifyByNavigation { get; set; }

    public virtual ICollection<RoomSeat> RoomSeats { get; set; } = new List<RoomSeat>();

    public virtual ICollection<StaffHistory> StaffHistories { get; set; } = new List<StaffHistory>();

    public virtual ICollection<Student> StudentEntryByNavigations { get; set; } = new List<Student>();

    public virtual ICollection<Student> StudentModifiedByNavigations { get; set; } = new List<Student>();

    public virtual ICollection<UserCredential> UserCredentialEntryByNavigations { get; set; } = new List<UserCredential>();

    public virtual ICollection<UserCredential> UserCredentialModifyByNavigations { get; set; } = new List<UserCredential>();

    public virtual UserCredential? UserCredentialStaff { get; set; }
}
