using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW_7_8.DAL.Entities
{
    public class Category
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [ForeignKey("user_id")]
        public IdentityUser? User { get; set; }
    }
}