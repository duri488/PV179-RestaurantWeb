namespace RestaurantWebInfrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
