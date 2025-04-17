using System.ComponentModel.DataAnnotations;

namespace CaoHub.Data.Models.Facturio
{
    public enum CalculationMethod
    {
        [Display(Name = "%")]
        PercentageBased = 1,

        [Display(Name = "$")]
        AdditiveValue = 2,
    }
}
