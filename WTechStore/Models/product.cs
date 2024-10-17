using System.ComponentModel.DataAnnotations.Schema;
using WTechStore.Models.CommonProp;

namespace WTechStore.Models
{
    public class product 
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductType { get; set; }
        public Decimal ProductPrice { get; set; }
        public string? ProductIMg { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        


    }
}
