using System;
using System.Linq;
using System.Threading.Tasks;
using Library.DTO;
using Library.Models;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        //GET : /api/Category
        public IActionResult Get()
        {

            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        //GET : /api/Category
        public IActionResult Post(CategoryCreate categoryCreate)
        {
            _categoryService.Create(categoryCreate);
            return Ok();
        }
    }
}