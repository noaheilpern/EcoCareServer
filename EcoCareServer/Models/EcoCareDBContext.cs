﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcoCareServer.Models
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

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryName)
                    .HasName("PK__Countrie__E056F200B0B6CA34");
            });

            modelBuilder.Entity<DatasCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__DatasCat__19093A0B15451248");
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.HasKey(e => e.DateT)
                    .HasName("PK__Goals__BFFD8573F71F2061");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Goals__UserName__37A5467C");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).ValueGeneratedNever();
            });

            modelBuilder.Entity<RegularUser>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__RegularU__C9F28457D6760617");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.RegularUser)
                    .HasForeignKey<RegularUser>(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RegularUs__UserN__29572725");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.SaleId).ValueGeneratedNever();

                entity.HasOne(d => d.BuyerUserNameNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.BuyerUserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__BuyerUser__2F10007B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__ProductId__300424B4");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Seller__C9F284577C1ED58B");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.Seller)
                    .HasForeignKey<Seller>(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seller__UserName__2C3393D0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Users__C9F28457E271D791");
            });

            modelBuilder.Entity<UsersDatum>(entity =>
            {
                entity.HasKey(e => e.DateT)
                    .HasName("PK__UsersDat__BFFD8573435E9DBB");

                entity.Property(e => e.DateT).ValueGeneratedNever();

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.UsersData)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersData__UserN__34C8D9D1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
