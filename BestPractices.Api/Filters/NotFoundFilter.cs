using BestPractices.Core.DTOs;
using BestPractices.Core.Entities;
using BestPractices.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BestPractices.Api.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {//buradakı next'in amacı herhangi bir filter'a takılmayacaksa next diyip bu request yoluna devam edecek.

            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return ;
            }
            var id = (int)idValue;
            var anyEntity= await _service.AnyAsync(x=>x.Id==id);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result=new NotFoundObjectResult(CustomResponseDTO<NoContentDTO>.Fail(404,$"{typeof(T).Name}({id}) not found"));
        }
    }
}
