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
    public class FoodModifier : BaseEntity
    {

        /// <summary>
        /// Khoá ngoại món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ForeigeKey]
        [NotUpdated]
        public Guid FoodId { get; set; }

        /// <summary>
        /// Khoá ngoại sở thích phục vụ
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ForeigeKey]
        [NotEmpty]
        public Guid ModifierId { get; set; }

        /// <summary>
        /// Tên sở thích phục vụ
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ReadOnly]
        public string ModifierName { get; set; }

        /// <summary>
        /// Phí phát sinh sở thích phục vụ
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ReadOnly]
        public int AdditionalCharge { get; set; }
    }
}
