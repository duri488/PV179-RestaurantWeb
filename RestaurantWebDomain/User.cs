namespace RestaurantWebDomain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}
