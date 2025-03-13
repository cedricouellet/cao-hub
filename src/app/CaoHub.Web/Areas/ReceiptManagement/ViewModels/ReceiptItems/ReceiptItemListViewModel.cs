using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels.ReceiptItems
{
    public class ReceiptItemListViewModel
    {
        public int ReceiptId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime ReceiptDate { get; set; }

        public string ReceiptPaidByPersonName { get; set; } = null!;

        public string ReceiptStoreName { get; set; } = null!;

        public IEnumerable<ReceiptItemViewModel> ReceiptItems { get; set; } = [];
    }
}
