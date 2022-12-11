using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using RestaurantWeb.Contract;

namespace RestaurantWebDAL.Models;

public class BaseEntity : IBaseEntity
{
    [Key]
    public int Id { get; set; }
}