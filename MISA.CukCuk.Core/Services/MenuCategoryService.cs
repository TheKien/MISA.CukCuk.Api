using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Infrastructure;
using MISA.CukCuk.Core.Interfaces.Service;
using MISA.CukCuk.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Services
{
    public class MenuCategoryService : BaseService<MenuCategory>, IMenuCategoryService
    {
        #region Contructor
        IMenuCategoryRepository _menuCategoryRepository;
        ServiceResult _serviceResult;
        public MenuCategoryService(IMenuCategoryRepository menuCategoryRepository) : base(menuCategoryRepository)
        {
            _menuCategoryRepository = menuCategoryRepository;
            _serviceResult = new ServiceResult() { Status = Resources.Status_Success };
        }
        #endregion
    }
}
