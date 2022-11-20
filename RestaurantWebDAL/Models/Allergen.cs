using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RestaurantWebDAL.Models;
[Index(nameof(NumberLocalizationCode), IsUnique = true)]
[Index(nameof(NameLocalizationCode), IsUnique = true)]
public class Allergen : BaseEntity
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string NumberLocalizationCode { get; set; }
    public string NameLocalizationCode { get; set; }
}