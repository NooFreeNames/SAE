using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SAE_DB
{
    public partial class SAEDBContext : DbContext
    {
        public SAEDBContext()
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
        }

        public SAEDBContext(DbContextOptions<SAEDBContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Discoverer> Discoverers { get; set; } = null!;
        public virtual DbSet<Exoplanet> Exoplanets { get; set; } = null!;
        public virtual DbSet<ExoplanetDetectionMethod> ExoplanetDetectionMethods { get; set; } = null!;
        public virtual DbSet<ExoplanetType> ExoplanetTypes { get; set; } = null!;
        public virtual DbSet<ResearchGroup> ResearchGroups { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<Star> Stars { get; set; } = null!;
        public virtual DbSet<StarDetectionMethod> StarDetectionMethods { get; set; } = null!;
        public virtual DbSet<StarType> StarTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseMySql(Properties.Resources.ConnectionString, ServerVersion.Parse(Properties.Resources.ServerVersion));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Discoverer>(entity =>
            {
                entity.ToTable("discoverer");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("mediumint unsigned");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<Exoplanet>(entity =>
            {
                entity.ToTable("exoplanet");

                entity.HasIndex(e => e.Name, "Exoplanet_Name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Discoverer, "fk_EXOPLANET__DISCOVERER_idx");

                entity.HasIndex(e => e.DetectionMethod, "fk_EXOPLANET__EXOPLANET_DETECTION_METHOD_idx");

                entity.HasIndex(e => e.Type, "fk_EXOPLANET__EXOPLANET_TYPE_idx");

                entity.HasIndex(e => e.UserWhoAdded, "fk_EXOPLANET__USER_WHO_ADDED_idx");

                entity.HasIndex(e => e.UserWhoConfirmed, "fk_EXOPLANET__USER_WHO_CONFIRMED_idx");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeConfirmation).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Discoverer).HasColumnType("mediumint unsigned");

                entity.Property(e => e.Name).HasMaxLength(35);

                entity.Property(e => e.Status).HasColumnType("enum('Confirmed','NotConfirmed')");

                entity.Property(e => e.UserWhoAdded).HasColumnType("mediumint unsigned");

                entity.Property(e => e.UserWhoConfirmed).HasColumnType("mediumint unsigned");

                entity.HasOne(d => d.DetectionMethodNavigation)
                    .WithMany(p => p.Exoplanets)
                    .HasForeignKey(d => d.DetectionMethod)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_EXOPLANET__EXOPLANET_DETECTION_METHOD");

                entity.HasOne(d => d.DiscovererNavigation)
                    .WithMany(p => p.Exoplanets)
                    .HasForeignKey(d => d.Discoverer)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_EXOPLANET__DISCOVERER");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Exoplanets)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_EXOPLANET__EXOPLANET_TYPE");

                entity.HasOne(d => d.UserWhoAddedNavigation)
                    .WithMany(p => p.ExoplanetUserWhoAddedNavigations)
                    .HasForeignKey(d => d.UserWhoAdded)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_EXOPLANET__USER_WHO_ADDED");

                entity.HasOne(d => d.UserWhoConfirmedNavigation)
                    .WithMany(p => p.ExoplanetUserWhoConfirmedNavigations)
                    .HasForeignKey(d => d.UserWhoConfirmed)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_EXOPLANET__USER_WHO_CONFIRMED");
            });

            modelBuilder.Entity<ExoplanetDetectionMethod>(entity =>
            {
                entity.ToTable("exoplanet_detection_method");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<ExoplanetType>(entity =>
            {
                entity.ToTable("exoplanet_type");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<ResearchGroup>(entity =>
            {
                entity.ToTable("research_group");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("mediumint unsigned");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("session");

                entity.HasIndex(e => e.User, "fk_SESSION_DATA__USER_idx");

                entity.Property(e => e.DeviceIdHash).HasMaxLength(68);

                entity.Property(e => e.User).HasColumnType("mediumint unsigned");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("fk_SESSION_DATA__USER");
            });

            modelBuilder.Entity<Star>(entity =>
            {
                entity.ToTable("star");

                entity.HasIndex(e => e.Name, "Star_Name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Discoverer, "fk_STAR__DISCOVERER_idx");

                entity.HasIndex(e => e.DetectionMethod, "fk_STAR__STAR_DETECTION_METHOD_idx");

                entity.HasIndex(e => e.Type, "fk_STAR__STAR_TYPE_idx");

                entity.HasIndex(e => e.UserWhoAdded, "fk_STAR__USER_WHO_ADDED_idx");

                entity.HasIndex(e => e.UserWhoConfirmed, "fk_STAR__USER_WHO_CONFIRMED_idx");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeConfirmation).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Discoverer).HasColumnType("mediumint unsigned");

                entity.Property(e => e.Name).HasMaxLength(35);

                entity.Property(e => e.Status).HasColumnType("enum('Confirmed','NotConfirmed')");

                entity.Property(e => e.UserWhoAdded).HasColumnType("mediumint unsigned");

                entity.Property(e => e.UserWhoConfirmed).HasColumnType("mediumint unsigned");

                entity.HasOne(d => d.DetectionMethodNavigation)
                    .WithMany(p => p.Stars)
                    .HasForeignKey(d => d.DetectionMethod)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_STAR__STAR_DETECTION_METHOD");

                entity.HasOne(d => d.DiscovererNavigation)
                    .WithMany(p => p.Stars)
                    .HasForeignKey(d => d.Discoverer)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_STAR__DISCOVERER");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Stars)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_STAR__STAR_TYPE");

                entity.HasOne(d => d.UserWhoAddedNavigation)
                    .WithMany(p => p.StarUserWhoAddedNavigations)
                    .HasForeignKey(d => d.UserWhoAdded)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_STAR__USER_WHO_ADDED");

                entity.HasOne(d => d.UserWhoConfirmedNavigation)
                    .WithMany(p => p.StarUserWhoConfirmedNavigations)
                    .HasForeignKey(d => d.UserWhoConfirmed)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_STAR__USER_WHO_CONFIRMED");

                entity.HasMany(d => d.Exoplanes)
                    .WithMany(p => p.Stars)
                    .UsingEntity<Dictionary<string, object>>(
                        "StarAndExoplanet",
                        l => l.HasOne<Exoplanet>().WithMany().HasForeignKey("Exoplane").HasConstraintName("fk_STAR_AND_EXOPLANET__EXOPLANET"),
                        r => r.HasOne<Star>().WithMany().HasForeignKey("Star").HasConstraintName("fk_STAR_AND_EXOPLANET__STAR"),
                        j =>
                        {
                            j.HasKey("Star", "Exoplane").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("star_and_exoplanet");

                            j.HasIndex(new[] { "Exoplane" }, "fk_STAR_AND_EXOPLANET__EXOPLANET_idx");

                            j.HasIndex(new[] { "Star" }, "fk_STAR_AND_EXOPLANET__STAR_idx");
                        });
            });

            modelBuilder.Entity<StarDetectionMethod>(entity =>
            {
                entity.ToTable("star_detection_method");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<StarType>(entity =>
            {
                entity.ToTable("star_type");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "Email_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("mediumint unsigned");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.PasswordHach).HasMaxLength(68);

                entity.Property(e => e.RegistrationDataTime).HasColumnType("datetime");

                entity.Property(e => e.TupeUser)
                    .HasColumnType("enum('None','Admin','Scientist')")
                    .HasDefaultValueSql("'None'");

                entity.HasMany(d => d.ResearchGroups)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserHasResearchGroup",
                        l => l.HasOne<ResearchGroup>().WithMany().HasForeignKey("ResearchGroup").HasConstraintName("fk_USER_HAS_RESEARCH_GROUP__RESEARCH_GROUP"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("User").HasConstraintName("fk_USER_HAS_RESEARCH_GROUP__USER"),
                        j =>
                        {
                            j.HasKey("User", "ResearchGroup").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("user_has_research_group");

                            j.HasIndex(new[] { "ResearchGroup" }, "fk_USER_HAS_RESEARCH_GROUP__RESEARCH_GROUP_idx");

                            j.HasIndex(new[] { "User" }, "fk_USER_HAS_RESEARCH_GROUP__USER_idx");

                            j.IndexerProperty<uint>("User").HasColumnType("mediumint unsigned");

                            j.IndexerProperty<uint>("ResearchGroup").HasColumnType("mediumint unsigned");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
