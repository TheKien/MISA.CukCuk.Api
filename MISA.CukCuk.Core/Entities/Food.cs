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
    public class Food : BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [NotUpdated]
        [Key]
        public Guid FoodId { get; set; }

        /// <summary>
        /// Mã món
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [NotEmpty]
        [Unique]
        [PropertyName("Mã món")]
        public string FoodCode { get; set; }

        /// <summary>
        /// Tên món
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [NotEmpty]
        [PropertyName("Tên món")]
        public string FoodName { get; set; }

        /// <summary>
        /// Thứ tự món
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public int? FoodOrder { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [NotEmpty]
        [PropertyName("Giá bán")]
        public int? SellingPrice { get; set; }

        /// <summary>
        /// Giá vốn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public int? CostPrice { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string Description { get; set; }

        /// <summary>
        /// Trạng thái hiển thị trên thực đơn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public int? DisplayStatus { get; set; }

        /// <summary>
        /// Khoá chính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid? ImageId { get; set; }

        /// <summary>
        /// Khoá ngoại nhóm món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid? FoodCategoryId { get; set; }

        /// <summary>
        /// Khoá ngoại nhóm thực đơn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid? MenuCategoryId { get; set; }

        /// <summary>
        /// Khoá ngoại đơn vị tính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [NotEmpty]
        [PropertyName("Đơn vị tính")]
        public Guid UnitId { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ReadOnly]
        public string UnitName { get; set; }

        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ReadOnly]
        public string MenuCategoryName { get; set; }

        /// <summary>
        /// Tên loại món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ReadOnly]
        public string FoodCategoryName { get; set; }

        /// <summary>
        /// Danh sách bếp chế biến của món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ReadOnly]
        public List<FoodKitchen> FoodKitchens { get; set; }

        /// <summary>
        /// Danh sách sở thích phục vụ của món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [ReadOnly]
        public List<FoodModifier> FoodModifiers { get; set; }
    }
}
