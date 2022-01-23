using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure.Repositories
{
    public class ModifierRepository : BaseRepository<Modifier>, IModifierRepository
    {
        #region Contructor
        public ModifierRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion
    }
}
