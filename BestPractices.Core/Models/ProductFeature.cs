using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Entities
{
    public class ProductFeature
    {
        //public ProductFeature(string color, Product product      //Nullable için
        // {                                                        // ancak projeozellıgı =>nullable=>disable
        //   Color=color ?? throw new ArgumentNullException(nameof(color));
        //     Product = product ?? throw new ArgumentNullException(nameof(product));
        // }
        public int Id { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
