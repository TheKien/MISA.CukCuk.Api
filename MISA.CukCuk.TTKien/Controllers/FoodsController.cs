using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Dtos;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Infrastructure;
using MISA.CukCuk.Core.Interfaces.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    public class FoodsController : BaseController<Food>
    {
        readonly IFoodService _FoodService;
        public FoodsController(IFoodService FoodService) : base(FoodService)
        {
            _FoodService = FoodService;
        }

        [HttpGet("PagingFilterSort")]
        public IActionResult GetPagingFilterSort([FromQuery]int pageIndex, int pageSize, string objectFilters, string objectSort)
        {
            try
            {
                var res = _FoodService.GetPagingFilterSort(pageIndex, pageSize, objectFilters, objectSort);
                return Ok(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
