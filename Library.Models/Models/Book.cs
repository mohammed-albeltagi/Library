using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
