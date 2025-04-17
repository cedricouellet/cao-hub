using CaoHub.Data.Models.Facturio;
using CaoHub.Web.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace CaoHub.Web.Areas.Facturio.Services
{
    public class CalculationMethodService(IStringLocalizer<SharedResource> localizer)
    {
        private readonly IStringLocalizer _localizer = localizer;

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return Enum.GetValues<CalculationMethod>().Select(x => new SelectListItem
            {
                Value = ((int)x).ToString(),
                Text = _localizer[$"Facturio_CalculationMethods_{x}"],
            });
        }

        public bool Exists(CalculationMethod calculationMethod)
        {
            return Enum.IsDefined(calculationMethod);
        }

        public decimal CalculateValue(decimal value, CalculationMethod calculationMethod)
        {
            return calculationMethod switch
            {
                CalculationMethod.PercentageBased => value * 0.01m,
                CalculationMethod.AdditiveValue => value,
                _ => value,
            };
        }
    }
}
