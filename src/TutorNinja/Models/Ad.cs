using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorNinja.Models
{
    public class Ad
    {
        public int AdID { get; set; }        
        public int CategoryID { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Title { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Description { get; set; }
        [Display(Name = "Date Created")]
        public DateTime CreateDate { get; set; }

        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
    }
}
