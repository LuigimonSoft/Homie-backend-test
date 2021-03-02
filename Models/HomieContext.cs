using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homie_backend_test.Models
{
    public class HomieContext : DbContext
    {
        public HomieContext(DbContextOptions<HomieContext> options) : base(options)
        {

        }

        public virtual DbSet<Owners> Owners { get; set; }
        public virtual DbSet<OwnersPropertys> OwnersPropertys { get; set; }
        public virtual DbSet<Partners> Partners { get; set; }
        public virtual DbSet<Propertys> Propertys { get; set; }
        public virtual DbSet<RentalPrices> RentalPrices { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tenants> Tenants { get; set; }
        public virtual DbSet<PartnerTypes> PartnerTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owners>(entity =>
            {
                entity.HasKey(e => e.OwnerId);

                entity.Property(e => e.OwnerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AvailabilityFrom).HasColumnType("datetime");

                entity.Property(e => e.AvailabilityTo).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OwnersPropertys>(entity =>
            {
                entity.HasKey(e => e.OwnerPropertyId);

                entity.HasIndex(e => e.OwnerId)
                    .HasName("XIF1OwnersPropertys");

                entity.HasIndex(e => e.PropertyId)
                    .HasName("XIF2OwnersPropertys");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.OwnerId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.OwnersPropertys)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("R_4");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.OwnersPropertys)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("R_5");
            });

            modelBuilder.Entity<Partners>(entity =>
            {
                entity.HasKey(e => e.PartnerId);

                entity.HasIndex(e => e.PartnerTypeId)
                    .HasName("XIF1Partners");

                entity.Property(e => e.PartnerId).ValueGeneratedNever();

                entity.Property(e => e.Partner)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PartnerType)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.PartnerTypeId)
                    .HasConstraintName("R_6");
            });

            modelBuilder.Entity<PartnerTypes>(entity =>
            {
                entity.HasKey(e => e.PartnerTypeId);

                entity.Property(e => e.PartnerType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Propertys>(entity =>
            {
                entity.HasKey(e => e.PropertyId);

                entity.HasIndex(e => e.StatusId)
                    .HasName("XIF2Propertys");

                entity.HasIndex(e => e.TenantId)
                    .HasName("XIF3Propertys");

                entity.Property(e => e.PropertyId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Propertys)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_2");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Propertys)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("R_3");
            });

            modelBuilder.Entity<RentalPrices>(entity =>
            {
                entity.HasKey(e => e.RentalPriceId);

                entity.HasIndex(e => e.PropertyId)
                    .HasName("XIF1RentalPrices");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RentalPrice).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.RentalPrices)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_7");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusId).ValueGeneratedNever();

                entity.Property(e => e.StatusText)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tenants>(entity =>
            {
                entity.HasKey(e => e.TenantId);

                entity.Property(e => e.TenantId).ValueGeneratedNever();

                entity.Property(e => e.AvailabilityFrom).HasColumnType("datetime");

                entity.Property(e => e.AvailabilityTo).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}