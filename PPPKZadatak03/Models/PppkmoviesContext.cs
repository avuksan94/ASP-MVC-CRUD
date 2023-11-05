using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PPPKZadatak03.Models;

public partial class PppkmoviesContext : DbContext
{
    public PppkmoviesContext()
    {
    }

    public PppkmoviesContext(DbContextOptions<PppkmoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Poster> Posters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Name=ConnectionStrings:PPPKConnString");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actor__57B3EA4BBECC14B1");

            entity.ToTable("Actor");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasMany(d => d.Movies).WithMany(p => p.Actors)
                .UsingEntity<Dictionary<string, object>>(
                    "ActorMovie",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ActorMovi__Movie__5070F446"),
                    l => l.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ActorMovi__Actor__4F7CD00D"),
                    j =>
                    {
                        j.HasKey("ActorId", "MovieId").HasName("PK__ActorMov__E30EC30AD176116D");
                        j.ToTable("ActorMovie");
                    });
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK__Director__26C69E46CAB3871D");

            entity.ToTable("Director");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasMany(d => d.Movies).WithMany(p => p.Directors)
                .UsingEntity<Dictionary<string, object>>(
                    "DirectorMovie",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DirectorM__Movie__5441852A"),
                    l => l.HasOne<Director>().WithMany()
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DirectorM__Direc__534D60F1"),
                    j =>
                    {
                        j.HasKey("DirectorId", "MovieId").HasName("PK__Director__927BB707D64224C6");
                        j.ToTable("DirectorMovie");
                    });
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__4BD2941AE0DEAA52");

            entity.ToTable("Movie");

            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.ReleaseDate).HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasMany(d => d.Posters).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MoviePoster",
                    r => r.HasOne<Poster>().WithMany()
                        .HasForeignKey("PosterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MoviePost__Poste__59FA5E80"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MoviePost__Movie__59063A47"),
                    j =>
                    {
                        j.HasKey("MovieId", "PosterId").HasName("PK__MoviePos__0119A5D2C7061A95");
                        j.ToTable("MoviePoster");
                    });
        });

        modelBuilder.Entity<Poster>(entity =>
        {
            entity.HasKey(e => e.PosterId).HasName("PK__Poster__ACB31C854BD8AE24");

            entity.ToTable("Poster");

            entity.Property(e => e.Content).HasMaxLength(1024);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
