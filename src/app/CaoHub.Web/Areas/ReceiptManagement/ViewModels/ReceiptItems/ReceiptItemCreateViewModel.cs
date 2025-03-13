using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels.ReceiptItems
{
    public class ReceiptItemCreateViewModel
    {
        [Required]
        public int? ReceiptId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? ProductName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 999999.999)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal? UnitPrice { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, 999999.999)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal? UnitDiscount { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; } = 1;

        public List<int> TaxIds { get; set; } = [];

        public List<int> PeopleIds { get; set; } = [];

        public IEnumerable<SelectListItem> Products { get; set; } = [];

        public IEnumerable<SelectListItem> Taxes { get; set; } = [];

        public IEnumerable<SelectListItem> People { get; set; } = [];
    }
}
