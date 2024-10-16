using System.ComponentModel.DataAnnotations;

namespace WTechStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Navigation property to store order items
        public ICollection<OrderItem> OrderItems { get; set; }
    }
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
    }
}
