using BestPractices.Core.Entities;
using System.Linq.Expressions;

namespace BestPractices.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); //asenkron merhod oldugu ıcın async yazıldı

        IQueryable<T> GetAll();//IEnumarable kullanmıyorum

        IQueryable<T> Where(Expression<Func<T,bool>> expression); //tolist , tolistascync çağırısak o zaman db ye gıder.
        //productRepository.where(x=>x.id>5).orderby.toListAsync(); satırında eger List olarak belirlentseeyydı
        //where den sonra dırekt datayı ceker, memoriyi alır ve meoroıytı aldıktan sonra orderby yapardı.
        //ama ıqureable ıle where ıfadesınden sorna orderby'ı da alır.

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        //delegeler methodları ısaret eden yapılardır.

        Task addAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> Entities);
        void Update(T entity);

        void Remove(T entity);  //aseenkron programlamayı var olan treklerı bloklamamak ıcın kullanıyoruz.

        void RemoveRange(IEnumerable<T> entities);


        List<Category> List();//deneniyor

    }
}
