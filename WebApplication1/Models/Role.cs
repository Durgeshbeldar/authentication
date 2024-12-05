using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace JWTdemo.Models
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public String RoleName { get; set; }
        public List<User> Users { get; set; }
     
    }
}
