using Microsoft.AspNetCore.Components;
using Trabajo_Practico_Final_2026.Services.Implementations;
using Trabajo_Practico_Final_2026.Services.Interfaces;

namespace Trabajo_Practico_Final_2026.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        /* public UsersController(IUserService userService, ICategoryService categoryService, IProductService productService)
        //{
            _userService = userService;
            _categoryService = categoryService;
            _productService = productService;
        }*/
    
    }
}
