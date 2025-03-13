using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels.Receipts
{
    public class ReceiptCreateViewModel
    {
        [Required]
        public int? PaidByPersonId { get; set; }

        [Required]
        public int? StoreId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; } = DateTime.Now;

        public IEnumerable<SelectListItem> Stores { get; set; } = [];

        public IEnumerable<SelectListItem> People { get; set; } = [];
    }
}
