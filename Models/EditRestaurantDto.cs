﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class EditRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
    }
}
