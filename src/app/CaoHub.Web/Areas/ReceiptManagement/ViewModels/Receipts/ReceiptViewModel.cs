using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels.Receipts
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = null!;
        public string PaidByPersonName { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }
    }
}
