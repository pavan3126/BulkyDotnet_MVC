﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyRazor_Temp.Models
{
    public class Category {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order Should Be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}