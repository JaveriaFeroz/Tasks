using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SoftechUpdateTask.Models;

public partial class ProjectContext : DbContext
{
    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<StudentInfo> StudentInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Order)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("order");
        });

        modelBuilder.Entity<StudentInfo>(entity =>
        {
            entity.ToTable("student_info");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.StudentName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("student_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
