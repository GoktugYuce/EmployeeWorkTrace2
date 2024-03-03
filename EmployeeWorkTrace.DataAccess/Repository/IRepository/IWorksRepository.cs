using EmployeeWorkTrace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.DataAccess.Repository.IRepository
{
    public interface IWorksRepository : IRepository<Works>
    {
        void Update(Works obj);

        Works GetFirstOrDefault(Expression<Func<Works, bool>> filter);
        IEnumerable<Works> GetAll(string includeProperties = null);
    }
}
