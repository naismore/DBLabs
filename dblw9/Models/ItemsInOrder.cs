﻿using System.ComponentModel.DataAnnotations;

namespace dblw9.Models
{
    public class ItemsInOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item ID is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Order ID is required.")]
        public int OrderId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one.")]
        public int Quantity { get; set; }

        public virtual Item? Item { get; set; }

        public virtual Order? Order { get; set; }
    }
}
