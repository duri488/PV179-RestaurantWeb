namespace RestaurantWeb.Contract
{
    public interface IUnitOfWorkFactory
    {
        public IUnitOfWork Build();
    }
}
