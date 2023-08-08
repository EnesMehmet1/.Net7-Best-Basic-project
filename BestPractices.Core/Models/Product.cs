using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Entities
{
    public class Product : BaseEntity
    {
        //[Required(ErrorMessage =)] //fluentValidation ile yapıyoruz bu kısımları
        public string Name { get; set; }
        //[Range(1,100)] //FluentValidation ile yapıyoruz
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } //Ef core categoryId dıyınce anlıyor ama custom verirsek [forign] diye alt satıra belirmemiz gerek. 
        public Category Category { get; set; }
        public ProductFeature ProductFeature { get; set; }
    }
}
