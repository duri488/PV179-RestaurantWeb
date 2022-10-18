using System.Linq.Expressions;

namespace RestaurantWebInfrastructure.Interfaces
{
    public interface IQuery<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Adds a possiblity to filter the result
        /// </summary>
        IQuery<TEntity> Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName) where T : IComparable<T>;

        /// <summary>
        /// Adds a specified sort criteria to the query.
        /// </summary>
        IQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

        /// <summary>
        /// Adds a posibility to paginate the result
        /// </summary>
        IQuery<TEntity> Page(int pageToFetch, int pageSize = 10);

        /// <summary>
        /// Executes the query and returns the results.
        /// </summary>
        IEnumerable<TEntity> Execute();
    }
}
