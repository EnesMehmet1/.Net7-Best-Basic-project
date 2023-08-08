using BestPractices.Core.UnitOfWorks;
using BEstPractices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync(); //sonuna result koyarak senktorna da çevirleiblir bu durumu track'ı bloklar.
        }
    }
}
