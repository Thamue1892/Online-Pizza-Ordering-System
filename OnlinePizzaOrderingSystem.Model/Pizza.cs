using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlinePizzaOrderingSystem.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Pizza Name")]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
