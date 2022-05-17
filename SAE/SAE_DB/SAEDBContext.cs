using Microsoft.EntityFrameworkCore;
using SAE_DB.Properties;

namespace SAE_DB
{
    public partial class SAEDBContext : DbContext
    {

        public virtual DbSet<Discoverer> Discoverers { get; set; } = null!;
        public virtual DbSet<Exoplanet> Exoplanets { get; set; } = null!;
        public virtual DbSet<ExoplanetDetectionMethod> ExoplanetDetectionMethods { get; set; } = null!;
        public virtual DbSet<ExoplanetType> ExoplanetTypes { get; set; } = null!;
        public virtual DbSet<ResearchGroup> ResearchGroups { get; set; } = null!;
        public virtual DbSet<Star> Stars { get; set; } = null!;
        public virtual DbSet<StarDetectionMethod> StarDetectionMethods { get; set; } = null!;
        public virtual DbSet<StarType> StarTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        public SAEDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Resources.ConnectionString, ServerVersion.Parse(Resources.ServerVersion));
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

                entity.HasIndex(e => e.User, "fk_EXOPLANET__USER_idx");

                entity.Property(e => e.Name).HasMaxLength(35);

                entity.HasOne(d => d.DetectionMethodNavigation)
                    .WithMany(p => p.Exoplanets)
                    .HasForeignKey(d => d.DetectionMethod)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_EXOPLANET__EXOPLANET_DETECTION_METHOD");

                entity.HasOne(d => d.DiscovererNavigation)
                    .WithMany(p => p.Exoplanets)
                    .HasForeignKey(d => d.Discoverer)
                    .HasConstraintName("fk_EXOPLANET__DISCOVERER");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Exoplanets)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_EXOPLANET__EXOPLANET_TYPE");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Exoplanets)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_EXOPLANET__USER");
            });

            modelBuilder.Entity<ExoplanetDetectionMethod>(entity =>
            {
                entity.ToTable("exoplanet_detection_method");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<ExoplanetType>(entity =>
            {
                entity.ToTable("exoplanet_type");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<ResearchGroup>(entity =>
            {
                entity.ToTable("research_group");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<Star>(entity =>
            {
                entity.ToTable("star");

                entity.HasIndex(e => e.Name, "Star_Name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Discoverer, "fk_STAR__DISCOVERER_idx");

                entity.HasIndex(e => e.DetectionMethod, "fk_STAR__STAR_DETECTION_METHOD_idx");

                entity.HasIndex(e => e.Type, "fk_STAR__STAR_TYPE_idx");

                entity.HasIndex(e => e.User, "fk_STAR__USER_idx");

                entity.Property(e => e.Name).HasMaxLength(35);

                entity.HasOne(d => d.DetectionMethodNavigation)
                    .WithMany(p => p.Stars)
                    .HasForeignKey(d => d.DetectionMethod)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_STAR__STAR_DETECTION_METHOD");

                entity.HasOne(d => d.DiscovererNavigation)
                    .WithMany(p => p.Stars)
                    .HasForeignKey(d => d.Discoverer)
                    .HasConstraintName("fk_STAR__DISCOVERER");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Stars)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_STAR__STAR_TYPE");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Stars)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_STAR__USER");

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

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<StarType>(entity =>
            {
                entity.ToTable("star_type");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "Email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ResearchGroup, "fk_USER__RESEARCH_GROUP_idx");

                entity.HasIndex(e => e.TupeUser, "fk_USER__USER_TYPE_idx");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.HasOne(d => d.ResearchGroupNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ResearchGroup)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_USER__RESEARCH_GROUP");

                entity.HasOne(d => d.TupeUserNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TupeUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_USER__USER_TYPE");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("user_type");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
