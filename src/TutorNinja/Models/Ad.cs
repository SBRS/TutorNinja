using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorNinja.Models
{
    public class Ad
    {
        public int AdID { get; set; }        
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
    }
}
