using MISA.CukCuk.API.Controllers;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    public class ModifiersController : BaseController<Modifier>
    {
        #region Contructor
        readonly IModifierService _modifierService;
        public ModifiersController(IModifierService modifierService) : base(modifierService)
        {
            _modifierService = modifierService;
        }
        #endregion
    }
}
