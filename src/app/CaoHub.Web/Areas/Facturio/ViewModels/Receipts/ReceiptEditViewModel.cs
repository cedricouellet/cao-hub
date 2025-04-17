using CaoHub.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.Facturio.ViewModels.Receipts
{
    public class ReceiptEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Facturio_Receipts_PaidByPerson", ResourceType = typeof(SharedResource))]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]   
        public int PaidByPersonId { get; set; }

        [Display(Name = "Facturio_Stores_Singular", ResourceType = typeof(SharedResource))]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        public int StoreId { get; set; }

        [Display(Name = "Facturio_Receipts_PrepaidAmount", ResourceType = typeof(SharedResource))]
        [Range(0.001, 999999.999)]
        [DataType(DataType.Currency)]
        public decimal? PrepaidAmount { get; set; }

        [Display(Name = "Facturio_Receipts_DiscountAmount", ResourceType = typeof(SharedResource))]
        [Range(0.001, 999999.999)]
        [DataType(DataType.Currency)]
        public decimal? DiscountAmount { get; set; }

        [Display(Name = "Date", ResourceType = typeof(SharedResource))]
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
