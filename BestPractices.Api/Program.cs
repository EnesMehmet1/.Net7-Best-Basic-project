using Autofac;
using Autofac.Extensions.DependencyInjection;
using BestPractices.Api.Filters;
using BestPractices.Api.MiidleWares;
using BestPractices.Api.Modules;
using BestPractices.Api.Validations;
using BestPractices.Core.Repositories;
using BestPractices.Core.Services;
using BestPractices.Core.UnitOfWorks;
using BestPractices.Repository.Repositories;
using BestPractices.Repository.UnitOfWorks;
using BestPractices.Service.Mapping;
using BestPractices.Service.Services;
using BestPractices.Service.Validations;
using BEstPractices.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//option kýsmý validaton(Filter tarafý) klasoyu ýcýn
builder.Services.AddControllers(Options => Options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>()); //fluentValidation için


builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true); //validation için default vadlýydounu kaldýrýp filtreyi eklýyoruz


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMemoryCache(); //caching için



builder.Services.AddScoped(typeof(NotFoundFilter<>));  //kendi oluþturdughumuz notfoundFilter'ý kullanabilmek için dedik.


//builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();   //AutofAc
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); //AutofAc
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));  //AutofAc



builder.Services.AddAutoMapper(typeof(MapProfile)); //býrden fazla olustudusaydýk baglý oldugu ýnterfacesý de verebilirdik?
//MapProductPrfýle,mapCategoryPfotýle vs




//builder.Services.AddScoped<IProductRepository,ProductRepository>(); //bu kýsýmlar autofac ile tek satýrda çözücez. //AutofAc
//builder.Services.AddScoped<IProductService,ProductService>();  //AutofAc


//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();//bu kýsýmlar autofac ile tek satýrda çözücez. //AutofAc
// builder.Services.AddScoped<ICategoryService, CategoryService>();  




builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);  // bu kýsým repository paketýnde oldugumuz ýcýn
    });
});


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder=>containerBuilder.RegisterModule(new RepoServiceModule()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  //app ile baþlayanlar mýddýleware




app.UserCustomException(); //Bu kendi yazdýgýmýz exception middelWare



app.UseAuthorization();

app.MapControllers();

app.Run();
