using BestPractices.Core.Entities;
using BestPractices.Core.Repositories;
using BEstPractices.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();  //ilgili veritabanını alıyo
        }

        //public GenericRepository(AppDbContext context, DbSet<T> dbSet) //bu hata epey ugrastırdı
        //{                                                               // 500 server hatası aldım dbset boyle oldugu ocon
        //_context = context;
        //_dbSet = dbSet;



        public async Task addAsync(T entity) //çünkü asenkron
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> Entities)
        {
           await _dbSet.AddRangeAsync(Entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
           return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable(); //ef core cekmıs oldugu dataları memory'e almıyor yanı track etmıyor AsnoTracking ile.
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
           //_context.Entry(entity).State = EntityState.Modified; // alt satır bunun ile aynı şey
            _dbSet.Remove(entity); //Asenkron methodu yok, Çünkü burada ilgili kısma memoryde track ediyor. sılınecek dıye
            //sonra da oradan siliniyor.
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public List<Category> List() //deneme idi
        {
            return _context.Categories.ToList();

        }

    }
}
