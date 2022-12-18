using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PV179_RestaurantWeb.Models;

public class WeeklyMenuCreateModel
{
    [DataType(DataType.Date)]
    public DateTime DateFrom { get; set; }
    [DataType(DataType.Date)]
    [Remote(action:"VerifyDateRange", controller:"WeeklyMenu", AdditionalFields = (nameof(DateFrom)), HttpMethod = "POST")]
    public DateTime DateTo { get; set; }
}