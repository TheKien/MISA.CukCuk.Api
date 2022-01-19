using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.CukCuk.Core.Attributes.AttributeCRUD;
using static MISA.CukCuk.Core.Attributes.AttributeKey;
using static MISA.CukCuk.Core.Attributes.AttributeValidate;

namespace MISA.CukCuk.Core.Entities
{
    public class FoodKitchen : BaseEntity
    {
        /// <summary>
        /// Khoá ngoại món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ForeigeKey]
        [NotUpdated]
        public Guid FoodId { get; set; }

        /// <summary>
        /// Khoá ngoại bếp
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ForeigeKey]
        [NotEmpty]
        public Guid KitchenId { get; set; }
    }
}
