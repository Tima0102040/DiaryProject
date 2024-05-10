using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Models;

public partial class DiaryProjectContext : DbContext
{
    public DiaryProjectContext()
    {
    }

    public DiaryProjectContext(DbContextOptions<DiaryProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admin { get; set; }
    
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }

    public virtual DbSet<Class> Class { get; set; }

    public virtual DbSet<ClassSubject> ClassSubjects { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Student> Student { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teacher { get; set; }

    public virtual DbSet<TeacherSubject> TeacherSubjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=DiaryProject;Persist Security Info=False;User ID=SA;Password=Valuetech@123;MultipleActiveResultSets=\nFalse;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity
                .HasKey(e => e.AdminId).HasName("Admin_pk");

            entity.Property(e => e.AdminEmail).HasMaxLength(100);
            entity.Property(e => e.AdminLogin).HasMaxLength(100);
            entity.Property(e => e.AdminPassword).HasMaxLength(100);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927C083A57AC8");

            entity.ToTable("Class");

            entity.Property(e => e.ClassName).HasMaxLength(1);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Class__TeacherId__3C69FB99");
        });

        modelBuilder.Entity<ClassSubject>(entity =>
        {
            entity.HasKey(e => e.ClassSubjectId).HasName("PK__ClassSub__79A97359BA6C2063");

            entity.ToTable("ClassSubject");

            entity.HasOne(d => d.Class).WithMany(p => p.ClassSubjects)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClassSubj__Class__4316F928");

            entity.HasOne(d => d.Subject).WithMany(p => p.ClassSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClassSubj__Subje__4222D4EF");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lesson__B084ACD0109CE573");

            entity.ToTable("Lesson");

            entity.Property(e => e.LessonDate).HasColumnType("datetime");
            entity.Property(e => e.LessonTopic).HasMaxLength(1);

            entity.HasOne(d => d.ClassSubject).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ClassSubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lesson__ClassSub__4E88ABD4");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.MarkId).HasName("PK__Mark__4E30D3668F5A841B");

            entity.ToTable("Mark");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Marks)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mark_Lesson");

            entity.HasOne(d => d.Student).WithMany(p => p.Marks)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mark_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B995E5F4A90");

            entity.ToTable("Student");

            entity.Property(e => e.StudentBirthDate).HasColumnType("datetime");
            entity.Property(e => e.StudentEmail).HasMaxLength(1);
            entity.Property(e => e.StudentFullName).HasMaxLength(1);
            entity.Property(e => e.StudentLogin).HasMaxLength(1);
            entity.Property(e => e.StudentPassword).HasMaxLength(1);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Student__ClassId__6E01572D");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__AC1BA3A86BAEAE10");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectName).HasMaxLength(1);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teacher__EDF259640ADE6708");

            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherEmail).HasMaxLength(1);
            entity.Property(e => e.TeacherLogin).HasMaxLength(1);
            entity.Property(e => e.TeacherPassword).HasMaxLength(1);
            entity.Property(e => e.TeacherFullName).HasMaxLength(1);
        });

        modelBuilder.Entity<TeacherSubject>(entity =>
        {
            entity.HasKey(e => e.TeacherSubjectId).HasName("PK__TeacherS__FB4DA4462ED7A363");

            entity.ToTable("TeacherSubject");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeacherSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeacherSu__Subje__49C3F6B7");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherSubjects)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeacherSu__Teach__4AB81AF0");
        });
        

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
