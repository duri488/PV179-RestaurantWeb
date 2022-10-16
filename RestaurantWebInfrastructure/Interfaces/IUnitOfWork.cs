using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebInfrastructure.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}
