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


//option k�sm� validaton(Filter taraf�) klasoyu �c�n
builder.Services.AddControllers(Options => Options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>()); //fluentValidation i�in


builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true); //validation i�in default vadl�ydounu kald�r�p filtreyi ekl�yoruz


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMemoryCache(); //caching i�in



builder.Services.AddScoped(typeof(NotFoundFilter<>));  //kendi olu�turdughumuz notfoundFilter'� kullanabilmek i�in dedik.


//builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();   //AutofAc
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); //AutofAc
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));  //AutofAc



builder.Services.AddAutoMapper(typeof(MapProfile)); //b�rden fazla olustudusayd�k bagl� oldugu �nterfaces� de verebilirdik?
//MapProductPrf�le,mapCategoryPfot�le vs




//builder.Services.AddScoped<IProductRepository,ProductRepository>(); //bu k�s�mlar autofac ile tek sat�rda ��z�cez. //AutofAc
//builder.Services.AddScoped<IProductService,ProductService>();  //AutofAc


//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();//bu k�s�mlar autofac ile tek sat�rda ��z�cez. //AutofAc
// builder.Services.AddScoped<ICategoryService, CategoryService>();  




builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);  // bu k�s�m repository paket�nde oldugumuz �c�n
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

app.UseHttpsRedirection();  //app ile ba�layanlar m�dd�leware




app.UserCustomException(); //Bu kendi yazd�g�m�z exception middelWare



app.UseAuthorization();

app.MapControllers();

app.Run();
