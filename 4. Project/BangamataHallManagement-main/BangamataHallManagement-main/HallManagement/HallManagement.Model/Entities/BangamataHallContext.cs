using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HallManagement.Model.Entities;

public partial class BangamataHallContext : DbContext
{
    public BangamataHallContext()
    {
    }

    public BangamataHallContext(DbContextOptions<BangamataHallContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<BloodGroup> BloodGroups { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<EstablishmentCharge> EstablishmentCharges { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<PasswordResetHistory> PasswordResetHistories { get; set; }

    public virtual DbSet<Religion> Religions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomSeat> RoomSeats { get; set; }

    public virtual DbSet<RoomSeatHistory> RoomSeatHistories { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffHistory> StaffHistories { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentHistory> StudentHistories { get; set; }

    public virtual DbSet<UserCredential> UserCredentials { get; set; }

    public virtual DbSet<UserCredentialHistory> UserCredentialHistories { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=SHARIF-PC\\MSSQLSERVER2019;User Id=bangamatahalldev;Password=bangamatahalldev;Database=BangamataHall;Encrypt=false;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.ToTable("Batch");

            entity.HasIndex(e => e.Name, "IX_Batch").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
        });

        modelBuilder.Entity<BloodGroup>(entity =>
        {
            entity.ToTable("BloodGroup");

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Classes");

            entity.ToTable("Class");

            entity.HasIndex(e => e.Name, "IX_Class").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.HasIndex(e => e.Name, "IX_Department").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.ToTable("Designation");

            entity.HasIndex(e => e.Name, "IX_Designation").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
        });

        modelBuilder.Entity<EstablishmentCharge>(entity =>
        {
            entity.ToTable("EstablishmentCharge");

            entity.HasIndex(e => new { e.StudentId, e.Year }, "IX_EstablishmentChargeStudentIdYear").IsUnique();

            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.EstablishmentChargeEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .HasConstraintName("FK_EstablishmentCharge_Staff");

            entity.HasOne(d => d.ModifyByNavigation).WithMany(p => p.EstablishmentChargeModifyByNavigations)
                .HasForeignKey(d => d.ModifyBy)
                .HasConstraintName("FK_EstablishmentCharge_Staff1");

            entity.HasOne(d => d.Student).WithMany(p => p.EstablishmentCharges)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_EstablishmentCharge_Student");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.ParentMenu).WithMany(p => p.InverseParentMenu)
                .HasForeignKey(d => d.ParentMenuId)
                .HasConstraintName("FK_Menu_MenuParent");
        });

        modelBuilder.Entity<MenuRole>(entity =>
        {
            entity.ToTable("MenuRole");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuRoles)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK_MenuRole_Menu");

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_MenuRole_Role");
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.ToTable("Nationality");

            entity.HasIndex(e => e.Name, "IX_Nationality").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<PasswordResetHistory>(entity =>
        {
            entity.ToTable("PasswordResetHistory");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDateTime).HasColumnType("datetime");
            entity.Property(e => e.HashedPasswordResetLink)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Religion>(entity =>
        {
            entity.ToTable("Religion");

            entity.HasIndex(e => e.Name, "IX_Religion").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Room");

            entity.HasIndex(e => e.RoomNo, "IX_Room").IsUnique();

            entity.Property(e => e.RoomNo)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
        });

        modelBuilder.Entity<RoomSeat>(entity =>
        {
            entity.ToTable("RoomSeat", tb => tb.HasTrigger("RoomSeatAuditTrigger"));

            entity.HasIndex(e => new { e.RoomId, e.SeatNo }, "IX_RoomSeatRoomIdSeatNo").IsUnique();

            entity.HasIndex(e => e.StudentId, "IX_RoomSeatStudentId")
                .IsUnique()
                .HasFilter("([StudentId] IS NOT NULL)");

            entity.Property(e => e.SeatNo)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomSeats)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomSeat_Room");

            entity.HasOne(d => d.Student).WithOne(p => p.RoomSeat)
                .HasForeignKey<RoomSeat>(d => d.StudentId)
                .HasConstraintName("FK_RoomSeat_Student");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.RoomSeats)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_RoomSeat_Staff");
        });

        modelBuilder.Entity<RoomSeatHistory>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("RoomSeatHistory");

            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.SeatNo).HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.RoomSeatHistories)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomSeatHistory_RoomSeat");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.ToTable("Section");

            entity.HasIndex(e => e.Name, "IX_Section").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session");

            entity.HasIndex(e => e.Name, "IX_Session").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("StaffAuditTrigger"));

            entity.HasIndex(e => e.Email, "IX_StaffEmail")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.HasIndex(e => e.Mobile, "IX_StaffMobile")
                .IsUnique()
                .HasFilter("([Mobile] IS NOT NULL)");

            entity.Property(e => e.ActingDateFrom).HasColumnType("datetime");
            entity.Property(e => e.ActingDateTo).HasColumnType("datetime");
            entity.Property(e => e.BioLink)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.Mobile)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Staff_Department");

            entity.HasOne(d => d.Designation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DesignationId)
                .HasConstraintName("FK_Staff_Designation");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.InverseEntryByNavigation)
                .HasForeignKey(d => d.EntryBy)
                .HasConstraintName("FK_Staff_Staff");

            entity.HasOne(d => d.ModifyByNavigation).WithMany(p => p.InverseModifyByNavigation)
                .HasForeignKey(d => d.ModifyBy)
                .HasConstraintName("FK_Staff_Staff1");
        });

        modelBuilder.Entity<StaffHistory>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("StaffHistory");

            entity.Property(e => e.ActingDateFrom).HasColumnType("datetime");
            entity.Property(e => e.ActingDateTo).HasColumnType("datetime");
            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BioLink)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.Mobile).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(250);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.StaffHistories)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffHistory_Staff");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student", tb => tb.HasTrigger("StudentAuditTrigger"));

            entity.HasIndex(e => e.ClassRollNo, "IX_StudentClassRollNo")
                .IsUnique()
                .HasFilter("([ClassRollNo] IS NOT NULL)");

            entity.HasIndex(e => e.Email, "IX_StudentEmail")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.HasIndex(e => e.Mobile, "IX_StudentMobile")
                .IsUnique()
                .HasFilter("([Mobile] IS NOT NULL)");

            entity.Property(e => e.ClassRollNo)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.FatherName)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.Mobile)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.MotherName)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.NameInEnglish)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNo)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");
            entity.Property(e => e.RegistrationYear)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AI");

            entity.HasOne(d => d.Batch).WithMany(p => p.Students)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK_Student_Batch");

            entity.HasOne(d => d.BloodGroup).WithMany(p => p.Students)
                .HasForeignKey(d => d.BloodGroupId)
                .HasConstraintName("FK_Student_BloodGroup");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_Student_Classes");

            entity.HasOne(d => d.Department).WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Student_Department");

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.StudentEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .HasConstraintName("FK_Student_StaffEntry");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.StudentModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK_Student_StaffModify");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Students)
                .HasForeignKey(d => d.NationalityId)
                .HasConstraintName("FK_Student_Nationality");

            entity.HasOne(d => d.Religion).WithMany(p => p.Students)
                .HasForeignKey(d => d.ReligionId)
                .HasConstraintName("FK_Student_Religion");

            entity.HasOne(d => d.Section).WithMany(p => p.Students)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK_Student_Section");

            entity.HasOne(d => d.Session).WithMany(p => p.Students)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_Student_Session");
        });

        modelBuilder.Entity<StudentHistory>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("StudentHistory");

            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ClassRollNo).HasMaxLength(100);
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.FatherName).HasMaxLength(250);
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.Mobile).HasMaxLength(100);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.MotherName).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.NameInEnglish)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNo).HasMaxLength(100);
            entity.Property(e => e.RegistrationYear).HasMaxLength(100);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.StudentHistories)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentHistory_Student");
        });

        modelBuilder.Entity<UserCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserCredentials");

            entity.ToTable("UserCredential", tb => tb.HasTrigger("UserCredentialAuditTrigger"));

            entity.HasIndex(e => e.StaffId, "IX_UserCredentialStaffId").IsUnique();

            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.EntryByNavigation).WithMany(p => p.UserCredentialEntryByNavigations)
                .HasForeignKey(d => d.EntryBy)
                .HasConstraintName("FK_UserCredentials_StaffEntry");

            entity.HasOne(d => d.ModifyByNavigation).WithMany(p => p.UserCredentialModifyByNavigations)
                .HasForeignKey(d => d.ModifyBy)
                .HasConstraintName("FK_UserCredentials_StaffModify");

            entity.HasOne(d => d.Role).WithMany(p => p.UserCredentials)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCredentials_Role");

            entity.HasOne(d => d.Staff).WithOne(p => p.UserCredentialStaff)
                .HasForeignKey<UserCredential>(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCredential_Staff");
        });

        modelBuilder.Entity<UserCredentialHistory>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("UserCredentialHistory");

            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.UserCredentialHistories)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCredentialHistory_UserCredential");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
