using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScanIT.ViewModels
{
    public class RecipeDetailsVM
    {
        public int? RecipeId { get; set; }

        [Display(Name = "Recipe")]
        public string RecipeName { get; set; }

        [Display(Name = "Preparation Time")]
        public int? PreparationTime { get; set; }

        [Display(Name = "Execution Time")]
        public int? ExecutionTime { get; set; }

        [Display(Name = "Level Of Difficulty")]
        public int? DifficultyLevel { get; set; }

        public IEnumerable<ProductVM> ProductVMs { get; set; }
    }
}