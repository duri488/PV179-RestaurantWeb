using System.ComponentModel.DataAnnotations;

namespace RestaurantWebDAL.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}