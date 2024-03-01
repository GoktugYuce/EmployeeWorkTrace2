using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using EmployeeWorkTrace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.DataAccess.Repository
{
    public class WorkersRepository : Repository<Workers>, IWorkersRepository
    {
        private DataContext _db;
        public WorkersRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Workers obj)
        {
            _db.Workers.Update(obj);
        }

        public Workers GetFirstOrDefault(Expression<Func<Workers, bool>> filter)
        {
            IQueryable<Workers> query = _db.Workers;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }


    }
}
