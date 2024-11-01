namespace WTechStore.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }  // Primary key for OrderProduct
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation property for products in the order
        public List<Product> Products { get; set; }

        public class Product
        {
            public int Id { get; set; }  // Primary key for Product
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

            // Foreign key to link back to OrderProduct if needed
            public int OrderProductId { get; set; }
            public OrderProduct OrderProduct { get; set; }
        }
    }
}
