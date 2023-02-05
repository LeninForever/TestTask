using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestTask.DAL
{
    public partial class StudydbContext : DbContext
    {
        public StudydbContext()
        {
        }

        public StudydbContext(DbContextOptions<StudydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<GroupsView> GroupsViews { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<OrganizationsEmployeeGroup> OrganizationsEmployeeGroups { get; set; } = null!;

        public virtual DbSet<StudyGroup> StudyGroups { get; set; } = null!;
        public virtual DbSet<StudyGroupsEmployee> StudyGroupsEmployees { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        public virtual DbSet<StudyGroupTeacher> StudyGroupTeachers { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LRKV1;Database=studydb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .HasColumnName("course_name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fio)
                    .HasMaxLength(150)
                    .HasColumnName("FIO");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__employees__organ__2F10007B");
            });

            modelBuilder.Entity<GroupsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("groups_view");

                entity.Property(e => e.EmployeeCount).HasColumnName("employee_count");

                entity.Property(e => e.StudyGroupId).HasColumnName("study_group_id");

                entity.Property(e => e.StudyGroupName)
                    .HasMaxLength(50)
                    .HasColumnName("study_group_name");

                entity.Property(e => e.TeacherFio)
                    .HasMaxLength(150)
                    .HasColumnName("teacher_fio");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("organizations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Inn)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("inn")
                    .IsFixedLength();

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(50)
                    .HasColumnName("organization_name");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__organizat__teach__2C3393D0");
            });

            modelBuilder.Entity<OrganizationsEmployeeGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("organizations_employee_groups");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Fio)
                    .HasMaxLength(150)
                    .HasColumnName("FIO");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(50)
                    .HasColumnName("organization_name");

                entity.Property(e => e.StudyGroupId).HasColumnName("study_group_id");
            });


            modelBuilder.Entity<StudyGroup>(entity =>
            {
                entity.ToTable("study_groups");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.StudyGroupName)
                    .HasMaxLength(50)
                    .HasColumnName("study_group_name");

                entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudyGroups)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__study_gro__cours__29572725");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.StudyGroups)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__study_gro__teach__286302EC");
            });

            modelBuilder.Entity<StudyGroupsEmployee>(entity =>
            {
                entity.ToTable("study_groups_employees");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.StudyGroupId).HasColumnName("study_group_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.StudyGroupsEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__study_gro__emplo__44FF419A");

                entity.HasOne(d => d.StudyGroup)
                    .WithMany(p => p.StudyGroupsEmployees)
                    .HasForeignKey(d => d.StudyGroupId)
                    .HasConstraintName("FK__study_gro__study__440B1D61");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teachers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fio)
                    .HasMaxLength(150)
                    .HasColumnName("fio");
            });
            modelBuilder.Entity<StudyGroupTeacher>(entity =>
                {
                    entity.HasNoKey();
                    entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
                    entity.Property(e => e.StudyGroupId).HasColumnName("study_group_id");
                    entity.Property(e => e.StudyGroupName).HasColumnName("study_group_name");
                  
                }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}