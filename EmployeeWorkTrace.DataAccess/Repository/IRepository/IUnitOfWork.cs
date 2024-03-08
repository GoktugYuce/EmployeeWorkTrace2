using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IWorksRepository Works { get; }
        IWorkersRepository Workers { get; }
        void Save();
    }
}


