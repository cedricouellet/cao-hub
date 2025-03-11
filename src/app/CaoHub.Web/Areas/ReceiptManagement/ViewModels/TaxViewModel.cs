namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels
{
    public class TaxViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Rate { get; set; }
    }
}
