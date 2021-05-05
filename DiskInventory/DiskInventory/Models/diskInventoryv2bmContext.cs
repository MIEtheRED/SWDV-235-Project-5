using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiskInventory.Models
{
    public partial class diskInventoryv2bmContext : DbContext
    {
        public diskInventoryv2bmContext()
        {
        }

        public diskInventoryv2bmContext(DbContextOptions<diskInventoryv2bmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Disk> Disks { get; set; }
        public virtual DbSet<DiskHasArtist> DiskHasArtists { get; set; }
        public virtual DbSet<DiskHasBorrower> DiskHasBorrowers { get; set; }
        public virtual DbSet<DiskType> DiskTypes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ViewIndividualArtist> ViewIndividualArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=diskInventoryv2bm;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artist");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .HasMaxLength(60)
                    .HasColumnName("lname");

                entity.HasOne(d => d.ArtistType)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artist__artist_t__31EC6D26");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.ToTable("artist_type");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("lname");

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_num");
            });

            modelBuilder.Entity<Disk>(entity =>
            {
                entity.ToTable("disk");

                entity.Property(e => e.DiskId).HasColumnName("disk_id");

                entity.Property(e => e.DiskName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("disk_name");

                entity.Property(e => e.DiskTypeId).HasColumnName("disk_type_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.DiskType)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.DiskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__disk_type___2F10007B");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__genre_id__2D27B809");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk__status_id__2E1BDC42");
            });

            modelBuilder.Entity<DiskHasArtist>(entity =>
            {
                entity.ToTable("disk_has_artist");

                entity.HasIndex(e => new { e.DiskId, e.ArtistId }, "UQ__disk_has__2DAE42F19DA8591D")
                    .IsUnique();

                entity.Property(e => e.DiskHasArtistId).HasColumnName("disk_has_artist_id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.DiskId).HasColumnName("disk_id");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.DiskHasArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___artis__3B75D760");

                entity.HasOne(d => d.Disk)
                    .WithMany(p => p.DiskHasArtists)
                    .HasForeignKey(d => d.DiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___disk___3A81B327");
            });

            modelBuilder.Entity<DiskHasBorrower>(entity =>
            {
                entity.ToTable("disk_has_borrower");

                entity.Property(e => e.DiskHasBorrowerId).HasColumnName("disk_has_borrower_id");

                entity.Property(e => e.BorrowedDate).HasColumnName("borrowed_date");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.DiskId).HasColumnName("disk_id");

                entity.Property(e => e.DueDate)
                    .HasColumnName("due_date")
                    .HasDefaultValueSql("(getdate()+(30))");

                entity.Property(e => e.ReturnedDate).HasColumnName("returned_date");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___borro__35BCFE0A");

                entity.HasOne(d => d.Disk)
                    .WithMany(p => p.DiskHasBorrowers)
                    .HasForeignKey(d => d.DiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_has___disk___36B12243");
            });

            modelBuilder.Entity<DiskType>(entity =>
            {
                entity.ToTable("disk_type");

                entity.Property(e => e.DiskTypeId).HasColumnName("disk_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<ViewIndividualArtist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Individual_Artist");

                entity.Property(e => e.ArtistId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("artist_id");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("fname");

                entity.Property(e => e.Lname)
                    .HasMaxLength(60)
                    .HasColumnName("lname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
