using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BillingProcessing.Charge.Domain.SeedWork
{
    public interface IMongoRepository<T>
    {
        IEnumerable<T> FilterBy(Expression<Func<T, bool>> filterExpression);

        Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression);

        Task<T> FindByIdAsync(string id);

        Task InsertOneAsync(T document);
    }
}
