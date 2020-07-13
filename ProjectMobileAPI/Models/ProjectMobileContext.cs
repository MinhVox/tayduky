using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectMobileAPI.Models
{
    public partial class ProjectMobileContext : DbContext
    {
        public ProjectMobileContext()
        {
        }

        public ProjectMobileContext(DbContextOptions<ProjectMobileContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAccount> TblAccount { get; set; }
        public virtual DbSet<TblActor> TblActor { get; set; }
        public virtual DbSet<TblScene> TblScene { get; set; }
        public virtual DbSet<TblSceneActor> TblSceneActor { get; set; }
        public virtual DbSet<TblSceneTool> TblSceneTool { get; set; }
        public virtual DbSet<TblTool> TblTool { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SE130010\\SQLEXPRESS;Database=ProjectMobile;uid=sa;password=minh0903149606");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<TblAccount>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_Account");

                entity.ToTable("tblAccount");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblActor>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_tblActor_1");

                entity.ToTable("tblActor");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Createtime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Img).HasMaxLength(100);

                entity.Property(e => e.Lastmodified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithOne(p => p.TblActor)
                    .HasForeignKey<TblActor>(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblActor_tblAccount");
            });

            modelBuilder.Entity<TblScene>(entity =>
            {
                entity.ToTable("tblScene");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Createtime).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(400);

                entity.Property(e => e.Director)
                    .HasColumnName("director")
                    .HasMaxLength(50);

                entity.Property(e => e.EndDay)
                    .HasColumnName("endDay")
                    .HasColumnType("date");

                entity.Property(e => e.FileDocOfRole)
                    .HasColumnName("fileDocOfRole")
                    .HasMaxLength(100);

                entity.Property(e => e.Lastmodified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberOfShotScenes).HasColumnName("numberOfShotScenes");

                entity.Property(e => e.StartDay)
                    .HasColumnName("startDay")
                    .HasColumnType("date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.DirectorNavigation)
                    .WithMany(p => p.TblScene)
                    .HasForeignKey(d => d.Director)
                    .HasConstraintName("FK_tblScene_tblAccount");
            });

            modelBuilder.Entity<TblSceneActor>(entity =>
            {
                entity.HasKey(e => new { e.Idscene, e.Username });

                entity.ToTable("tblSceneActor");

                entity.Property(e => e.Idscene).HasColumnName("IDscene");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.IdsceneNavigation)
                    .WithMany(p => p.TblSceneActor)
                    .HasForeignKey(d => d.Idscene)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSceneActor_tblScene");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.TblSceneActor)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSceneActor_tblActor1");
            });

            modelBuilder.Entity<TblSceneTool>(entity =>
            {
                entity.HasKey(e => new { e.Idscene, e.Idtool });

                entity.ToTable("tblSceneTool");

                entity.Property(e => e.Idscene).HasColumnName("IDscene");

                entity.Property(e => e.Idtool).HasColumnName("IDtool");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.IdsceneNavigation)
                    .WithMany(p => p.TblSceneTool)
                    .HasForeignKey(d => d.Idscene)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSceneTool_tblScene");

                entity.HasOne(d => d.IdtoolNavigation)
                    .WithMany(p => p.TblSceneTool)
                    .HasForeignKey(d => d.Idtool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSceneTool_tblTool");
            });

            modelBuilder.Entity<TblTool>(entity =>
            {
                entity.ToTable("tblTool");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Createtime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Img).HasMaxLength(50);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });
        }
    }
}
