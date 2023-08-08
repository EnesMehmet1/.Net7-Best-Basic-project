using BestPractices.Core.DTOs;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BestPractices.Api.Validations
{
    public class ValidateFilterAttribute : ActionFilterAttribute //Senkron
    {
        public override void OnActionExecuting(ActionExecutingContext context) //Senkron yapıyoruz
        {
            if (!context.ModelState.IsValid)
            { //eğer bir hata var ise içeriye giriyor
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDTO<NoContentDTO>.Fail(400, errors));

            }

        }
    }

}