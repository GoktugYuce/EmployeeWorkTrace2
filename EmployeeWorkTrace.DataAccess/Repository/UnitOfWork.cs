using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;

namespace EmployeeWorkTrace.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private DataContext _db;
        public IWorksRepository Works { get; private set; }
        public UnitOfWork(DataContext db)
        {
            _db = db;
            Works = new WorksRepository(_db); 
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
