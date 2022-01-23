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
    public class KitchensController : BaseController<Kitchen>
    {
        #region Contructor
        readonly IKitchenService _kitchenService;
        public KitchensController(IKitchenService kitchenService) : base(kitchenService)
        {
            _kitchenService = kitchenService;
        }
        #endregion
    }
}
