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

            entity.Ignore(e => e.Description);
            entity.Ignore(e => e.ShortDescription);

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CvDocumentUrl).HasColumnName("CvDocumentURL");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
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

            entity.Ignore(e => e.School);
            entity.Ignore(e => e.Department);
            entity.Ignore(e => e.Description);
            entity.Ignore(e => e.DateRange);
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("Experiences");
            
            entity.Ignore(e => e.Title);
            entity.Ignore(e => e.Company);
            entity.Ignore(e => e.Location);
            entity.Ignore(e => e.DateRange);
        });

        modelBuilder.Entity<ExperienceResponsibility>(entity =>
        {
            entity.ToTable("ExperienceResponsibilities");

            entity.Ignore(e => e.Description);

            entity.HasOne(d => d.Experience).WithMany(p => p.ExperienceResponsibilities)
                .HasForeignKey(d => d.ExperienceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ExperienceResponsibilities_Experiences");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Projects");

            entity.Ignore(e => e.Title);
            entity.Ignore(e => e.Description);

            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("Skills");

            entity.Ignore(e => e.Name);
            entity.Ignore(e => e.Category);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
