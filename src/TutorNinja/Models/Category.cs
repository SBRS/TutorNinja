using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorNinja.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Ad> Ads { get; set; }
    }
}
