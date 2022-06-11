using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoCareServerBL.Models
{
    public partial class EcoCareDBContext : DbContext
    {
        public EcoCareDBContext()
        {
        }

        public EcoCareDBContext(DbContextOptions<EcoCareDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DatasCategory> DatasCategories { get; set; }
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

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryName)
                    .HasName("PK__Countrie__E056F2000873E7B9");
            });

            modelBuilder.Entity<DatasCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__DatasCat__19093A0B7239044A");
            });

            modelBuilder.Entity<RegularUser>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__RegularU__C9F2845782B7903D");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.RegularUser)
                    .HasForeignKey<RegularUser>(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RegularUs__UserN__2C3393D0");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasOne(d => d.BuyerUserNameNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.BuyerUserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__BuyerUser__31EC6D26");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__ProductId__33D4B598");

                entity.HasOne(d => d.SellerUserNameNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.SellerUserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__SellerUse__32E0915F");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Seller__C9F2845713D9897D");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.Seller)
                    .HasForeignKey<Seller>(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seller__UserName__2F10007B");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Users__C9F284579901BA83");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__Country__29572725");
            });

            modelBuilder.Entity<UsersDatum>(entity =>
            {
                entity.HasKey(e => new { e.DateT, e.CategoryId, e.UserName })
                    .HasName("PK__UsersDat__48A4E4574E5E3A9F");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.UsersData)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersData__Categ__38996AB5");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UsersData)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersData__UserN__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
