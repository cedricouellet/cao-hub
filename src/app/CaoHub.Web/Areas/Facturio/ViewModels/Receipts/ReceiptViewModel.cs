using CaoHub.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.Facturio.ViewModels.Receipts
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Facturio_Receipts_PaidByPerson", ResourceType = typeof(SharedResource))]
        public string PaidByPersonName { get; set; } = null!;

        [Display(Name = "Facturio_Receipts_PrepaidAmount", ResourceType = typeof(SharedResource))]
        [DataType(DataType.Currency)]
        public decimal? PrepaidAmount { get; set; }

        [Display(Name = "Facturio_Receipts_DiscountAmount", ResourceType = typeof(SharedResource))]
        [DataType(DataType.Currency)]
        public decimal? DiscountAmount { get; set; }

        [Display(Name = "Facutio_Stores_Singular")]
        public string StoreName { get; set; } = null!;

        [Display(Name = "Date", ResourceType = typeof(SharedResource))]
        public DateTime Date { get; set; }
    }
}
