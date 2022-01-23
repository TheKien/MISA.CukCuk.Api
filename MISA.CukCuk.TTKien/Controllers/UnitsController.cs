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
    public class UnitsController : BaseController<Unit>
    {
        #region Contructor
        readonly IUnitService _unitService;
        public UnitsController(IUnitService unitService) : base(unitService)
        {
            _unitService = unitService;
        }
        #endregion
    }
}
