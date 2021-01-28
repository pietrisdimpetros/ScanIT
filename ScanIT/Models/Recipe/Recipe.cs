using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScanIT.Models.Recipe
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string RecipeName { get; set; }
        [Required]
        [Display(Name = "Preparation Time")]
        [Range(0, 99999)]
        public int PreparationTime { get; set; }
        [Required]
        [Display(Name = "Execution Time")]
        [Range(0, 99999)]
        public int ExecutionTime { get; set; }
        [Required]
        [Display(Name = "Level Of Difficulty")]
        [Range(1, 5)]
        public int DifficultyLevel { get; set; }



    }
}