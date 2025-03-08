using CaoHub.Web.Areas.ReceiptManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CaoHub.Web.Data
{
    /// <summary>
    /// The main data context for the CAOHub application
    /// </summary>
    /// <remarks>
    /// Instantiate a new <see cref="CaoHubDbContext"/> with database context options.
    /// </remarks>
    /// <param name="options">The options for the context</param>
    public class CaoHubDbContext(DbContextOptions<CaoHubDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the database set of store categories
        /// </summary>
        public virtual DbSet<StoreCategory> StoreCategories { get; set; }

        /// <summary>
        /// Gets or sets the database set of stores
        /// </summary>
        public virtual DbSet<Store> Stores { get; set; }

        /// <summary>
        /// Gets or sets the database set of receipts
        /// </summary>
        public virtual DbSet<Receipt> Receipts { get; set; }

        /// <summary>
        /// Gets or sets the database set of receipt items
        /// </summary>
        public virtual DbSet<ReceiptItem> ReceiptItems { get; set; }

        /// <summary>
        /// Gets or sets the database set of products
        /// </summary>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the database set of taxes
        /// </summary>
        public virtual DbSet<Tax> Taxes { get; set; }

        /// <summary>
        /// Gets or sets the database set of people
        /// </summary>
        public virtual DbSet<Person> People { get; set; }

        /// <summary>
        /// Gets or sets the database set of groupings between receipt items and people
        /// </summary>
        public virtual DbSet<ReceiptItemPerson> ReceiptItemsPeople { get; set; }

        /// <summary>
        /// Gets or sets the database set of groupings between receipt items and taxes
        /// </summary>
        public virtual DbSet<ReceiptItemTax> ReceiptItemsTaxes { get; set; }

        /// <summary>
        /// Configures the model before it is created.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReceiptItemPerson>()
                .HasOne(x => x.Person)
                .WithMany(x => x.ReceiptItemsPeople)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ReceiptItemPerson>()
                .HasOne(x => x.ReceiptItem)
                .WithMany(x => x.ReceiptItemsPeople)
                .HasForeignKey(x => x.ReceiptItemId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ReceiptItemTax>()
                .HasOne(x => x.Tax)
                .WithMany(x => x.ReceiptItemsTaxes)
                .HasForeignKey(x => x.TaxId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ReceiptItemTax>()
                .HasOne(x => x.ReceiptItem)
                .WithMany(x => x.ReceiptItemsTaxes)
                .HasForeignKey(x => x.ReceiptItemId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        /// <summary>
        /// Configures the db context's conventions
        /// </summary>
        /// <param name="configurationBuilder">The convention builder used to configure the conventions.</param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // This removes the default behaviour of EF Core that inferred the table name from the DbSet properties' names.
            // Instead, it will use the entity's class name instead.
            // Example "People" -> "Person".
            configurationBuilder.Conventions.Remove<TableNameFromDbSetConvention>();
        }
    }
}
