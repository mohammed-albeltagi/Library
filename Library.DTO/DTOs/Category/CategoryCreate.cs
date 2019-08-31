using System;

namespace Library.DTO
{
    public class CategoryCreate
    {
        public string Name { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
    }
}
