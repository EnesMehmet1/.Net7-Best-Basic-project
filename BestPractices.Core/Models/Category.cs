using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        //navigation proporty
        public ICollection<Product> Products { get; set; } //liste de kullanılabilir. Ne farkı var araştır
    }
}
