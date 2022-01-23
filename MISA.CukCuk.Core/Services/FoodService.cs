using MISA.CukCuk.Core.Dtos;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Infrastructure;
using MISA.CukCuk.Core.Interfaces.Service;
using MISA.CukCuk.Core.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class FoodService : BaseService<Food>, IFoodService
    {
        IFoodRepository _foodRepository;
        ServiceResult _serviceResult;
        public FoodService(IFoodRepository FoodRepository) : base(FoodRepository)
        {
            _foodRepository = FoodRepository;
            _serviceResult = new ServiceResult() { Status = Resources.Status_Success };
        }

        public object GetPagingFilterSort(int pageIndex, int pageSize, string objectFilters, string objectSort)
        {
            // Convert string json => list object
            List<ObjectFilter> objectFiltersJson = new List<ObjectFilter>();
            ObjectSort objectSortJson = new ObjectSort();

            objectFiltersJson = JsonConvert.DeserializeObject<List<ObjectFilter>>(objectFilters);

            if (objectSort == null)
            {
                objectSortJson = null;
            }
            else
            {
                objectSortJson = JsonConvert.DeserializeObject<ObjectSort>(objectSort);
            }

            return _foodRepository.GetPagingFilterSort(pageIndex, pageSize, objectFiltersJson, objectSortJson);
        }

    }
}
