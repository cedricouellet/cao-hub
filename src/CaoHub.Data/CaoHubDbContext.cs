using CaoHub.Data.ReceiptManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CaoHub.Data
{
    /// <summary>
    /// The main data context for the CaoHub application
    /// </summary>
    /// <param name="options">The DbContext options</param>
    public class CaoHubDbContext(DbContextOptions<CaoHubDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the store category entities set
        /// </summary>
        public virtual DbSet<StoreCategory> StoreCategories { get; set; }

        /// <summary>
        /// Gets or sets the store entities set
        /// </summary>
        public virtual DbSet<Store> Stores { get; set; }

        /// <summary>
        /// Gets or sets the receipt entities set
        /// </summary>
        public virtual DbSet<Receipt> Receipts { get; set; }
    }
}
