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
    public class ModifierService : BaseService<Modifier>, IModifierService
    {
        #region Contructor
        IModifierRepository _modifierRepository;
        ServiceResult _serviceResult;
        public ModifierService(IModifierRepository modifierRepository) : base(modifierRepository)
        {
            _modifierRepository = modifierRepository;
            _serviceResult = new ServiceResult() { Status = Resources.Status_Success };
        }
        #endregion
    }
}
