using CaoHub.Data.Models.Facturio;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Data
{
    public class CaoHubDbContext(DbContextOptions<CaoHubDbContext> options) : DbContext(options)
    {
        public virtual DbSet<StoreCategory> StoreCategories { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<ReceiptDetailPerson> ReceiptDetailsPeople { get; set; }
        public virtual DbSet<ReceiptDetailTax> ReceiptDetailsTaxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnFacturioModelCreating(modelBuilder);
        }

        private static void OnFacturioModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReceiptDetailPerson>()
                .HasOne(x => x.Person)
                .WithMany(x => x.ReceiptDetailsPeople)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ReceiptDetailPerson>()
                .HasOne(x => x.ReceiptDetail)
                .WithMany(x => x.ReceiptDetailsPeople)
                .HasForeignKey(x => x.ReceiptDetailId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ReceiptDetailTax>()
                .HasOne(x => x.Tax)
                .WithMany(x => x.ReceiptDetailsTaxes)
                .HasForeignKey(x => x.TaxId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ReceiptDetailTax>()
                .HasOne(x => x.ReceiptDetail)
                .WithMany(x => x.ReceiptDetailsTaxes)
                .HasForeignKey(x => x.ReceiptDetailId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
