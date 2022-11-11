using System.ComponentModel.DataAnnotations;

namespace RestaurantWebDomain;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}