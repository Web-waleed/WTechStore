using Microsoft.Build.Evaluation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTechStore.Models
{
    [Table("orderProducts")]
    public class OrderProduct
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType( DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ProductItem> Products { get; set; } = new List<ProductItem>();

    }
    public class ProductItem
    {
        public int Id { get; set; }
        public int OrderProductId { get; set; } 
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

      
        public OrderProduct OrderProduct { get; set; }
    }
}
