using BestPractices.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BEstPractices.Repository.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id); //fluent api //eger ıd degılde ısımlendırme ypasakydık //ve juzerıne [Key] yazmak yerınde boyle de yapabılırız best practices bu 
            builder.Property(x => x.Id).UseIdentityColumn(); //Defultu 1 1 artıyor ıstersek bu oranı değiştirebiliriz.
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50); // null olmayan max 50 karaketerlı name
            builder.ToTable("Categories");
        }
    }
}
