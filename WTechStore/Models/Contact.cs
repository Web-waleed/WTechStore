﻿using System.ComponentModel.DataAnnotations;

namespace WTechStore.Models
{
    public class Contact
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        [DataType(dataType: DataType.Date)]
        public DateTime DateSubmitted { get; set; } 

    }
}
