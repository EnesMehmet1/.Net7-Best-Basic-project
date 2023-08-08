using BestPractices.Core.Entities;
using BEstPractices.Repository.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BEstPractices.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
           // var  P= new Product() { ProductFeature =new ProductFeature() { } }
           //1-1 ilişkide boyle bır kullanımda varmıs ancak bunun ıcın dbset'ını yazmıyoruz
           //bıraz daha detayları arastırılabılır.
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration()); //tekb ır taneyı yapmak isteseydik kullanırdık
            // modelBuilder.Entity<Category>().HasKey(c => c.Id); //fluent api //eger ıd degılde ısımlendırme ypasakydık
            //BU işlemleri confıguratons klasorunun ıverınde yapıyoruz


            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature() //seed data
            {
                Id = 1,                   //veritabanı oluşturuurken otomatık gelmesını ıstedıgımız default dataları
                Color = "Red",           //Bu şekilde eklıyoruz
                Height = 12,             //ancak buradaya yazmak yerinde seed'ın ıcerısındede yazabılırdık.
                Weight = 12,
                ProductId = 1
            }, new ProductFeature()
            {
                Id = 2,
                Color = "blue",
                Height = 12,
                Weight = 12,
                ProductId = 2
            });
            base.OnModelCreating(modelBuilder);//ve juzerıne [Key] yazmak yerınde boyle de yapabılırız best practices bu 
        }
    }
}
