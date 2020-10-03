using System;
using FinanceManagement.DataRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinanceManagement.DataAccess.Context
{
    public partial class FinanceManagementContext : DbContext
    {
        public FinanceManagementContext()
        {
        }

        public FinanceManagementContext(DbContextOptions<FinanceManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public virtual DbSet<Period> Periods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("longtext");

                entity.Property(e => e.Percentage).HasColumnType("int(11)");
            });

            modelBuilder.Entity<FinancialTransaction>(entity =>
            {
                entity.ToTable("financialtransactions");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_CategoryId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("longtext");

                entity.Property(e => e.IsExpense).HasColumnType("tinyint(1)");

                entity.Property(e => e.Value).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Financialtransactions)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_FinancialTransactions_Categories_CategoryId");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("periods");

                entity.Property(e => e.Id).HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
