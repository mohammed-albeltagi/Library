using Library.DTO;
using Library.Models;
using Library.Repositories.Interfaces;
using Library.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public IEnumerable<CategorySummary> GetAll()
        {
            return _repository.GetAll().Select(x => new CategorySummary
            {
              Name = x.Name,
              Id = x.Id
            });
        }

        public CategoryDetails Get(int id, Category category)
        {
            var cat = _repository.Update(id, category);
            _repository.Save();

            return cat == null ? null : Map(cat);
        }

        public void Create(CategoryCreate category)
        {
            var CategoryEntity = new Category
            {
               Name = category.Name,
               ParentCategoryId = category.ParentCategoryId
            };

            _repository.Add(CategoryEntity);
            _repository.Save();
        }

        private static CategoryDetails Map(Task<Category> category)
        {
            return new CategoryDetails
            {
              
            };
        }
    }
}
