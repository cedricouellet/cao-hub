using CaoHub.Data.Models.Facturio;

namespace CaoHub.Web.Areas.Facturio.ViewModels.Taxes
{
    public class TaxViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public decimal Value { get; set; }

        public CalculationMethod CalculationMethod { get; set; }

        public string FormattedValue { get; set; } = null!;
    }
}
