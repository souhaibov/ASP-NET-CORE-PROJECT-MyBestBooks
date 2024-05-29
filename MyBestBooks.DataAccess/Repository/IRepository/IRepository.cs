using System.Linq.Expressions;

namespace MyBestBooks.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T-Category
        IEnumerable<T> GetAll(string? includeProperties = null); 
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
		// (string? includeProperties = null) at that point also we want to include properties so we will add that logic in the repository (if)
		void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}
