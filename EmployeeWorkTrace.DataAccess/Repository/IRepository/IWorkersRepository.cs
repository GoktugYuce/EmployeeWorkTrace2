using EmployeeWorkTrace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.DataAccess.Repository.IRepository
{
    public interface IWorkersRepository : IRepository<Workers>
    {
        void Update(Workers obj);
        Workers GetFirstOrDefault(Expression<Func<Workers, bool>> filter);
    }
}
