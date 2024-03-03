using EmployeeWorkTrace.DataAccess.Data;
using EmployeeWorkTrace.DataAccess.Repository.IRepository;
using EmployeeWorkTrace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.DataAccess.Repository
{
    public class WorksRepository : Repository<Works>, IWorksRepository
    {
        private DataContext _db;
        public WorksRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Works obj)
        {
            _db.Works.Update(obj);
        }

        public Works GetFirstOrDefault(Expression<Func<Works, bool>> filter)
        {
            IQueryable<Works> query = _db.Works;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<Works> GetAll(string includeProperties = null)
        {
            IQueryable<Works> query = _db.Works;

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }
    }
}
