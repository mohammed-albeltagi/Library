using Library.DTO;
using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategorySummary> GetAll();
        CategoryDetails Get(int id, Category category);
        void Create(CategoryCreate category);
    }
}
