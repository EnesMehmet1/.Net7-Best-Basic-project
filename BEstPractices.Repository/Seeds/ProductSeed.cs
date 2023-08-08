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
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id = 1,Name="Bilinmiyor 1" ,CategoryId = 1,Price=100,Stock=20,CreateDate=DateTime.Now },  //seed data olarak eklerken Identıty olmasına ragmen Id'yı eklemmeız gerekıyor.
                new Product { Id = 2,Name="Bilinmiyor 2",CategoryId = 3, Price = 10012, Stock = 20, CreateDate = DateTime.Now },
                new Product { Id = 3,Name="Bilinmiyor 3" ,CategoryId = 2, Price = 101240, Stock = 20, CreateDate = DateTime.Now }
                );
        }
    }
}
