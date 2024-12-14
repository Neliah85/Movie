using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Movie.Models;

public partial class MovieContext : DbContext
{
    public MovieContext()
    {
    }

    public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Mufaj> Mufajs { get; set; }

    public virtual DbSet<Rendezo> Rendezos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=movie;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("film");

            entity.HasIndex(e => e.MufajId, "MufajId");

            entity.HasIndex(e => e.RendezoId, "RendezoId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Cim).HasMaxLength(50);
            entity.Property(e => e.Hossz).HasColumnType("time(4)");
            entity.Property(e => e.IndexKep).HasColumnType("blob");
            entity.Property(e => e.Kep).HasColumnType("mediumblob");
            entity.Property(e => e.Korhatar).HasColumnType("int(2)");
            entity.Property(e => e.MufajId).HasColumnType("int(11)");
            entity.Property(e => e.OscarE).HasColumnName("Oscar_e");
            entity.Property(e => e.RendezoId).HasColumnType("int(11)");

            entity.HasOne(d => d.Mufaj).WithMany(p => p.Films)
                .HasForeignKey(d => d.MufajId)
                .HasConstraintName("film_ibfk_2");

            entity.HasOne(d => d.Rendezo).WithMany(p => p.Films)
                .HasForeignKey(d => d.RendezoId)
                .HasConstraintName("film_ibfk_1");
        });

        modelBuilder.Entity<Mufaj>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mufaj");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nev).HasMaxLength(32);
            entity.Property(e => e.RovidNev).HasMaxLength(8);
        });

        modelBuilder.Entity<Rendezo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rendezo");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nemzetiseg).HasMaxLength(32);
            entity.Property(e => e.Nev).HasMaxLength(64);
            entity.Property(e => e.SzulDatum).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
