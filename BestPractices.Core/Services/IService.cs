using BestPractices.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id); //asenkron merhod oldugu ıcın async yazıldı

        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> Where(Expression<Func<T, bool>> expression); //tolist , tolistascync çağırısak o zaman db ye gıder.
        //productRepository.where(x=>x.id>5).orderby.toListAsync(); satırında eger List olarak belirlentseeyydı
        //where den sonra dırekt datayı ceker, memoriyi alır ve meoroıytı aldıktan sonra orderby yapardı.
        //ama ıqureable ıle where ıfadesınden sorna orderby'ı da alır.

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        //delegeler methodları ısaret eden yapılardır.

        Task<T> addAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> Entities);
        Task UpdateAsync(T entity);  //Task asenkron

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);


    }
}
