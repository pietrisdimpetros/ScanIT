using System;
using System.ComponentModel.DataAnnotations;

namespace ScanIT.Models
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Gender")]
        public string GenderName { get; set; }
    }
}