using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.API.Controllers;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    public class MenuCategorysController : BaseController<MenuCategory>
    {
        #region Contructor
        readonly IMenuCategoryService _menuCategoryService;
        public MenuCategorysController(IMenuCategoryService menuCategoryService) : base(menuCategoryService)
        {
            _menuCategoryService = menuCategoryService;
        }
        #endregion
    }   
}
