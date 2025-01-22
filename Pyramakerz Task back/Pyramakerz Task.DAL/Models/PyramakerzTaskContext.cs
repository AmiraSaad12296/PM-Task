﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pyramakerz_Task.Models;

public partial class PyramakerzTaskContext : DbContext
{
    public PyramakerzTaskContext()
    {
    }

    public PyramakerzTaskContext(DbContextOptions<PyramakerzTaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acadimic_Year> Acadimic_Years { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAcademicYear> StudentAcademicYears { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PyramakerzTask;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acadimic_Year>(entity =>
        {
            entity.HasOne(d => d.School).WithMany(p => p.Acadimic_Years).HasConstraintName("FK_Acadimic_Year_School");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasOne(d => d.Academic_Year).WithMany(p => p.Classrooms).HasConstraintName("FK_Classroom_Acadimic_Year");

            entity.HasOne(d => d.Grade).WithMany(p => p.Classrooms).HasConstraintName("FK_Classroom_Grade");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasOne(d => d.Section).WithMany(p => p.Grades).HasConstraintName("FK_Grade_Section");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasOne(d => d.School).WithMany(p => p.Sections).HasConstraintName("FK_Section_School");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasOne(d => d.Academic_Year).WithMany(p => p.Semesters).HasConstraintName("FK_Semester_Acadimic_Year");
        });

        modelBuilder.Entity<StudentAcademicYear>(entity =>
        {
            entity.HasOne(d => d.Class).WithMany(p => p.StudentAcademicYears).HasConstraintName("FK_StudentAcademicYear_Classroom");

            entity.HasOne(d => d.Grade).WithMany(p => p.StudentAcademicYears).HasConstraintName("FK_StudentAcademicYear_Grade");

            entity.HasOne(d => d.School).WithMany(p => p.StudentAcademicYears).HasConstraintName("FK_StudentAcademicYear_School");

            entity.HasOne(d => d.Semester).WithMany(p => p.StudentAcademicYears).HasConstraintName("FK_StudentAcademicYear_Semester");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentAcademicYears).HasConstraintName("FK_StudentAcademicYear_Student");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}