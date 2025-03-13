namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels.ReceiptItems
{
    public class ReceiptItemViewModel
    {
        public int ReceiptId { get; set; }

        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal? UnitDiscount { get; set; }

        public IEnumerable<string> PeopleNames { get; set; } = [];

        public IEnumerable<string> TaxNames { get; set; } = [];
    }
}
