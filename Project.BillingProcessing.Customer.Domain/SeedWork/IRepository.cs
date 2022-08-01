using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BillingProcessing.Charge.Domain.SeedWork
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
