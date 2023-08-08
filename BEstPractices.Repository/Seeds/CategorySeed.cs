using BestPractices.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEstPractices.Repository.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
               new Category { Id = 1, Name = "Enes" },  //seed data olarak eklerken Identıty olmasına ragmen Id'yı eklemmeız gerekıyor.
               new Category { Id = 2, Name = "Mehmet" },
               new Category { Id = 3, Name = "Yıldırım" }
               );
        }
    }
}
