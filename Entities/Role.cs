﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
