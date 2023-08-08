using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Entities
{
    public abstract class BaseEntity
    {
        //ef core Id dıyınce otomatık anlıyor custom ısım verırısek [Key]diye vermemız gerekıyor.
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
