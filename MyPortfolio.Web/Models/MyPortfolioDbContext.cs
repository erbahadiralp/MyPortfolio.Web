using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Web.Models.Entities;

namespace MyPortfolio.Web.Models;

public partial class MyPortfolioDbContext : IdentityDbContext<Admin>
{
    public MyPortfolioDbContext(DbContextOptions<MyPortfolioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<ContactMessage> ContactMessages { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<ExperienceResponsibility> ExperienceResponsibilities { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // This is important for Identity tables

        modelBuilder.Entity<About>(entity =>
        {
            entity.ToTable("Abouts");

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CvDocumentUrl).HasColumnName("CvDocumentURL");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.ShortDescription).HasMaxLength(500);
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.ToTable("ContactMessages");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.SentDate).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(100);
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.ToTable("Educations");

            entity.Property(e => e.DateRange).HasMaxLength(50);
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.School).HasMaxLength(100);
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("Experiences");

            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.DateRange).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<ExperienceResponsibility>(entity =>
        {
            entity.ToTable("ExperienceResponsibilities");

            entity.HasOne(d => d.Experience).WithMany(p => p.ExperienceResponsibilities)
                .HasForeignKey(d => d.ExperienceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ExperienceResponsibilities_Experiences");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Projects");

            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("Skills");

            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
