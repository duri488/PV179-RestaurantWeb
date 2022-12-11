using Microsoft.AspNetCore.Identity;
using RestaurantWeb.Contract;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWebDAL.Models
{
    public class User : IdentityUser, IBaseEntity
    {
        public new int Id { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
    }
}
