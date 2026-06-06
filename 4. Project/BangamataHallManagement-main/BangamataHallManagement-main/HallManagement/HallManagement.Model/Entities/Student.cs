using System;
using System.Collections.Generic;

namespace HallManagement.Model.Entities;

public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? NameInEnglish { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? ClassRollNo { get; set; }

    public string? RegistrationNo { get; set; }

    public int? ClassId { get; set; }

    public int? DepartmentId { get; set; }

    public int? BatchId { get; set; }

    public int? SectionId { get; set; }

    public int? SessionId { get; set; }

    public string? RegistrationYear { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public int? ReligionId { get; set; }

    public int? NationalityId { get; set; }

    public int? BloodGroupId { get; set; }

    public string? Password { get; set; }

    public DateTime? EntryDate { get; set; }

    public int? EntryBy { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public bool? IsArchived { get; set; }

    public bool? IsPasswordResetDone { get; set; }

    public virtual Batch? Batch { get; set; }

    public virtual BloodGroup? BloodGroup { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Staff? EntryByNavigation { get; set; }

    public virtual ICollection<EstablishmentCharge> EstablishmentCharges { get; set; } = new List<EstablishmentCharge>();

    public virtual Staff? ModifiedByNavigation { get; set; }

    public virtual Nationality? Nationality { get; set; }

    public virtual Religion? Religion { get; set; }

    public virtual RoomSeat? RoomSeat { get; set; }

    public virtual Section? Section { get; set; }

    public virtual Session? Session { get; set; }

    public virtual ICollection<StudentHistory> StudentHistories { get; set; } = new List<StudentHistory>();
}
