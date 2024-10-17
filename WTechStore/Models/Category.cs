using System.ComponentModel.DataAnnotations.Schema;
using WTechStore.Models.CommonProp;

namespace WTechStore.Models
{
    public class Category 
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public String? CategoryImg { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public virtual ICollection<product> Products { get; set; } = new List<product>();
    }
}
