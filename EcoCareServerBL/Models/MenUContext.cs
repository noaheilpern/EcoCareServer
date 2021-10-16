using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoCareServerBL.Models
{
    public partial class MenUContext : DbContext
    {
        public MenUContext()
        {
        }

        public MenUContext(DbContextOptions<MenUContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RegularUser> RegularUsers { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersDatum> UsersData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = localhost\\SQLEXPRESS; Database=EcoCareDB; Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.HasKey(e => e.DateT)
                    .HasName("PK__Goals__BFFD8573B8A02356");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Goals__UserName__33D4B598");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).ValueGeneratedNever();
            });

            modelBuilder.Entity<RegularUser>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__RegularU__C9F2845718CF9FDF");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.SaleId).ValueGeneratedNever();

                entity.HasOne(d => d.BuyerUserNameNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.BuyerUserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__BuyerUser__2D27B809");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__ProductId__2E1BDC42");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Seller__C9F284578C30E689");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Users__C9F2845740B9A05B");
            });

            modelBuilder.Entity<UsersDatum>(entity =>
            {
                entity.HasKey(e => e.DateT)
                    .HasName("PK__UsersDat__BFFD85730CF4CAD9");

                entity.Property(e => e.DateT).ValueGeneratedNever();

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UsersData)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersData__UserN__30F848ED");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
