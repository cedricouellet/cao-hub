﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels
{
    public class StoreCreateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public int? StoreCategoryId { get; set; }

        public IEnumerable<SelectListItem> StoreCategoriesSelectListItems { get; set; } = [];
    }
}
