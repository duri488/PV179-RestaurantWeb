using System.Linq.Expressions;
using RestaurantWebDAL;
using RestaurantWebInfrastructure.Query;

namespace RestaurantWebInfrastructure.EFCore.Query
{
    public class Query<TEntity> : IQuery<TEntity> where TEntity : class, new()
    {
        protected RestaurantWebDbContext Dbcontext { get; set; }

        public List<(Expression expression, Type argumentType, string columnName)> WherePredicate { get; set; } = new();
        public (string tableName, bool isAscending, Type argumentType)? OrderByContainer { get; set; }
        public (int PageToFetch, int PageSize)? PaginationContainer { get; set; }

        public Query(RestaurantWebDbContext dbcontext)
        {
            Dbcontext = dbcontext;
        }

        public IQuery<TEntity> Page(int pageToFetch, int pageSize = 10)
        {
            PaginationContainer = (pageToFetch, pageSize);
            return this;
        }

        public IQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>
        {
            OrderByContainer = (columnName, ascendingOrder, typeof(T));
            return this;
        }

        // 1st argument -> table name
        public IQuery<TEntity> Where<T>(Expression<Func<T, bool>> predicate, string columnName) where T : IComparable<T>
        {
            WherePredicate.Add((predicate, typeof(T), columnName));
            return this;
        }

        public IEnumerable<TEntity> Execute()
        {
            IQueryable<TEntity> query = Dbcontext.Set<TEntity>();

            if (WherePredicate.Capacity != 0)
            {
                query = ApplyWhere(query);
            }

            if (OrderByContainer != null)
            {
                query = OrderBy(query);
            }

            if (PaginationContainer.HasValue)
            {
                query = Pagination(query);
            }

            return query.ToList();
        }

        private IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query)
        {
            foreach (var expr in WherePredicate)
            {
                // parameter for new lambda
                var p = Expression.Parameter(typeof(TEntity), "p");

                // get the property name from the etity. For instance, its the same as calling `nameof(Subject.Name)`
                var columnNameFromObject = typeof(TEntity)
                    .GetProperty(expr.columnName)
                    ?.Name;

                // basically creates the property call, i.e -> p.Name
                var exprProp = Expression.Property(p, columnNameFromObject);// nameof(Customer.CustomerID));
                                                                            // replace parameter in original expression
                var expression = expr.expression;

                // gets the Expression Parameters
                var parameters = (IReadOnlyCollection<ParameterExpression>)expression
                    .GetType()
                    .GetProperty("Parameters")
                    ?.GetValue(expression);

                // gets the expression body
                var body = (Expression)expression
                    .GetType()
                    .GetProperty("Body")
                    ?.GetValue(expression);

                // (both) replaces the old lambda parameter with the new one
                // Example:
                //      a => a > 10
                // ->   p => p.Price > 10
                var visitor = new ReplaceParamVisitor(parameters.First(), exprProp);
                var exprNewBody = visitor.Visit(body);

                // creates the new lambda expression
                var lambda = Expression.Lambda<Func<TEntity, bool>>(exprNewBody, p);

                query = query.Where(lambda);
            }

            return query;
        }

        private IQueryable<TEntity> OrderBy(IQueryable<TEntity> query)
        {
            var orderByColumn = OrderByContainer.Value.tableName;
            var isAscending = OrderByContainer.Value.isAscending;
            var argumentType = OrderByContainer.Value.argumentType;

            var p = Expression.Parameter(typeof(TEntity), "p");

            var columnNameFromObject = typeof(TEntity)
                .GetProperty(orderByColumn)
                ?.Name;

            var exprProp = Expression.Property(p, columnNameFromObject);
            var lambda = Expression.Lambda(exprProp, p);

            var orderByMethod = typeof(Queryable)
                .GetMethods()
                .First(a => a.Name == (isAscending ? "OrderBy" : "OrderByDescending") && a.GetParameters().Length == 2);

            var orderByClosedMethod = orderByMethod.MakeGenericMethod(typeof(TEntity), argumentType);

            return (IQueryable<TEntity>)orderByClosedMethod.Invoke(null, new object[] { query, lambda })!;
        }

        private IQueryable<TEntity> Pagination(IQueryable<TEntity> query)
        {
            var page = PaginationContainer.Value.PageToFetch;
            var pageSize = PaginationContainer.Value.PageSize;

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
