using Microsoft.AspNetCore.Identity;

namespace HW_7_8.BLL.Models
{
    public class CategoryDataModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public IdentityUser? User { get; set; }
    }
}