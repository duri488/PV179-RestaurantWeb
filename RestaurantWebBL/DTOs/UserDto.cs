namespace RestaurantWebBL.DTOs;

public class UserDto : BaseEntityDto
{
    public string Username { get; set; }
    public byte[] HashedPassword { get; set; }
    public byte[] Salt { get; set; }
}