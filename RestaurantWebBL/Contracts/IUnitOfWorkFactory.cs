namespace RestaurantWebBL.Contracts
{
    public interface IUnitOfWorkFactory
    {
        public IUnitOfWork Build();
    }
}
