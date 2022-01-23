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
    public class KitchenService : BaseService<Kitchen>, IKitchenService
    {
        #region Contructor
        IKitchenRepository _kitchenRepository;
        ServiceResult _serviceResult;
        public KitchenService(IKitchenRepository kitchenRepository) : base(kitchenRepository)
        {
            _kitchenRepository = kitchenRepository;
            _serviceResult = new ServiceResult() { Status = Resources.Status_Success };
        }
        #endregion
    }
}
