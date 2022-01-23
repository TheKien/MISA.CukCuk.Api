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
    public class UnitService : BaseService<Unit>, IUnitService
    {
        #region Contructor
        IUnitRepository _unitRepository;
        ServiceResult _serviceResult;
        public UnitService(IUnitRepository unitRepository) : base(unitRepository)
        {
            _unitRepository = unitRepository;
            _serviceResult = new ServiceResult() { Status = Resources.Status_Success };
        }
        #endregion
    }
}
