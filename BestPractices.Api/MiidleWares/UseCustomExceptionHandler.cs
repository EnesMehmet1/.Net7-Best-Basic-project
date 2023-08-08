using BestPractices.Core.DTOs;
using BestPractices.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Identity.Client;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BestPractices.Api.MiidleWares
{
    public static class UseCustomExceptionHandler
    {
        public static void UserCustomException(this IApplicationBuilder app)//IApplicationBuilder, WebApplication sınıfının implemente ettiği bir interfaces
        {
            app.UseExceptionHandler(config => // mıddle ware | |  | | | | bu 6 cubuk mıddleawre olsun bır hata olunca onun ıcı donuyor
            {


                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statuscode = exceptionFeature.Error switch   //eroor tipi
                    {
                        ClientSideException => 400,      //servies katmanında exception içinde oluşturduğumuz ClientSideException'dan mesaj gelıyor
                        NotFoundException =>404,
                        _ => 500
                    };

                    context.Response.StatusCode = statuscode;
                    var response = CustomResponseDTO<NoContentDTO>.Fail(statuscode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response)); //controller tarafında olmadıgı ıcın framerwok gıbı otomatık json a çevirmiyor
                });

            });
    }
    }
}
