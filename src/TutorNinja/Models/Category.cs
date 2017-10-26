using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TutorNinja.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        public ICollection<Ad> Ads { get; set; }
    }
}
