using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category SupCategory { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
