namespace RestaurantWebBL.DTOs;

public class UserDto
{
    public string Username { get; set; }
    public byte[] HashedPassword { get; set; }
    public byte[] Salt { get; set; }
}