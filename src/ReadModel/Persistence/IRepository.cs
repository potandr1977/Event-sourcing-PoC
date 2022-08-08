using ReadModel.Common;
using System.Threading.Tasks;

namespace ReadModel.Persistence
{
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
