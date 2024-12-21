using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aptech_Vision_Project.Models;

public partial class AptechVisionProjectContext : DbContext
{
    public AptechVisionProjectContext()
    {
    }

    public AptechVisionProjectContext(DbContextOptions<AptechVisionProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Courselevel> Courselevels { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Lecture> Lectures { get; set; }

    public virtual DbSet<Subscriber> Subscribers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userdetail> Userdetails { get; set; }

    public virtual DbSet<Userimage> Userimages { get; set; }

    public virtual DbSet<Website> Websites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Aptech_Vision_Project;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contact__3214EC077B782730");

            entity.ToTable("contact");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__course__3214EC072885F97F");

            entity.ToTable("course");

            entity.Property(e => e.Certificate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Courseduration).HasColumnName("courseduration");
            entity.Property(e => e.Coursefee).HasColumnName("coursefee");
            entity.Property(e => e.Courseimage)
                .IsUnicode(false)
                .HasColumnName("courseimage");
            entity.Property(e => e.Courseinstructor).HasColumnName("courseinstructor");
            entity.Property(e => e.Courselevelid).HasColumnName("courselevelid");
            entity.Property(e => e.Courselongdescription)
                .IsUnicode(false)
                .HasColumnName("courselongdescription");
            entity.Property(e => e.Coursename)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("coursename");
            entity.Property(e => e.Courserating).HasColumnName("courserating");
            entity.Property(e => e.Courseshortdescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("courseshortdescription");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("language");
            entity.Property(e => e.Lectures).HasColumnName("lectures");
            entity.Property(e => e.Studentsenrolled).HasColumnName("studentsenrolled");

            entity.HasOne(d => d.CourseinstructorNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Courseinstructor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_ToTable_1");

            entity.HasOne(d => d.Courselevel).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Courselevelid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_ToTable");
        });

        modelBuilder.Entity<Courselevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__coursele__3214EC07138C620E");

            entity.ToTable("courselevel");

            entity.Property(e => e.Courselevel1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("courselevel");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__instruct__3214EC078BD2CC23");

            entity.ToTable("instructor");

            entity.Property(e => e.Coursesteach).HasColumnName("coursesteach");
            entity.Property(e => e.Enrolledstudents).HasColumnName("enrolledstudents");
            entity.Property(e => e.Instructordescription)
                .IsUnicode(false)
                .HasColumnName("instructordescription");
            entity.Property(e => e.Instructoremail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instructoremail");
            entity.Property(e => e.Instructorimage)
                .IsUnicode(false)
                .HasColumnName("instructorimage");
            entity.Property(e => e.Instructorname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instructorname");
            entity.Property(e => e.Instructorrating).HasColumnName("instructorrating");
            entity.Property(e => e.Instructorreviews).HasColumnName("instructorreviews");
            entity.Property(e => e.Instructorrole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("instructorrole");
        });

        modelBuilder.Entity<Lecture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lecture__3214EC0761D1D6D5");

            entity.ToTable("Lecture");

            entity.Property(e => e.Courseid).HasColumnName("courseid");
            entity.Property(e => e.Lecture1)
                .IsUnicode(false)
                .HasColumnName("Lecture");

            entity.HasOne(d => d.Course).WithMany(p => p.LecturesNavigation)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_ToTable5");
        });

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subscrib__3214EC070256C069");

            entity.ToTable("subscribers");

            entity.Property(e => e.Subscribedemail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subscribedemail");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC074966676D");

            entity.ToTable("user");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Pass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('user')")
                .HasColumnName("role");
        });

        modelBuilder.Entity<Userdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userdeta__3214EC0700425ABA");

            entity.ToTable("userdetails");

            entity.Property(e => e.Address)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("city");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Mobile).HasColumnName("mobile");
            entity.Property(e => e.Qualification)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("qualification");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Userdetails)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_ToTable4");
        });

        modelBuilder.Entity<Userimage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userimag__3214EC0759823D46");

            entity.ToTable("userimage");

            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasDefaultValueSql("('/Admin/img/user.png')")
                .HasColumnName("image");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Userimages)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_course_ToTable3");
        });

        modelBuilder.Entity<Website>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Websites__3214EC0787603603");

            entity.Property(e => e.About1)
                .IsUnicode(false)
                .HasColumnName("about1");
            entity.Property(e => e.About2)
                .IsUnicode(false)
                .HasColumnName("about2");
            entity.Property(e => e.Carousel1)
                .IsUnicode(false)
                .HasColumnName("carousel1");
            entity.Property(e => e.Carousel2)
                .IsUnicode(false)
                .HasColumnName("carousel2");
            entity.Property(e => e.Logo)
                .IsUnicode(false)
                .HasColumnName("logo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
