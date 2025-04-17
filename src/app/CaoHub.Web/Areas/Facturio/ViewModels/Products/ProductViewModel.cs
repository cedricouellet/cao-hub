using CaoHub.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.Facturio.ViewModels.Products
{
    public class ProductViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(SharedResource))]
        public string Name { get; set; } = null!;
    }
}
