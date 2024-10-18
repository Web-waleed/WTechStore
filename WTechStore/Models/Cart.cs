﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WTechStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }         
        public string Image { get; set; }
       
        public string ProductName { get; set; }  
        public decimal Price { get; set; }       
        public int Quantity { get; set; }        
        public decimal Total => Price * Quantity;

    }
}
