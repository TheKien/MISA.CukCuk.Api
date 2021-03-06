using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Dtos;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Infrastructure.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        #region Contructor
        public FoodRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Method
        public override Food Get(Guid foodId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"$FoodId", foodId);
            var storeName = "Proc_GetFoodById";
            using (var multi = _dbConnection.QueryMultiple(storeName, parameters, commandType: CommandType.StoredProcedure))
            {
                var food =  multi.ReadSingleOrDefault<Food>();
                if (food != null)
                {
                    food.FoodModifiers = multi.Read<FoodModifier>().ToList();
                    food.FoodKitchens = multi.Read<FoodKitchen>().ToList();
                }
                return food;
            }
        }

        public object GetPagingFilterSort(int pageIndex, int pageSize, List<ObjectFilter> objectFilters, ObjectSort objectSort)
        {
            string where = BuildSqlWhere(objectFilters);
            string sort = BuildSqlSort(objectSort);
            DynamicParameters parameters = new();

            var storeName = "Proc_GetFoodsPagingFilterSort";
            parameters.Add("$PageSize", pageSize);
            parameters.Add("$PageIndex", pageIndex);
            parameters.Add("$Where", where);
            parameters.Add("$Sort", sort);
            parameters.Add("$TotalRecord", direction: ParameterDirection.Output);
            parameters.Add("$TotalPage", direction: ParameterDirection.Output);
            // Thực thi lấy dữ liệu trong db:
            var entities = _dbConnection.Query<Food>(storeName, param: parameters, commandType: CommandType.StoredProcedure);
            var totalPage = parameters.Get<int>("$TotalPage");
            var totalRecord = parameters.Get<int>("$TotalRecord");
            return new
            {
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                Data = entities
            };
        }

        /// <summary>
        /// Convert list objecFiter thành một câu lệnh điều kiện của WHERE
        /// </summary>
        /// <param name="objectFilters">Danh sách đối tượng tìm kiếm</param>
        /// <returns>Câu lệnh where trong sql</returns>
        /// CreatedBy: TTKien(17/01/2022)
        private static string BuildSqlWhere(List<ObjectFilter> objectFilters)
        {
            string where = string.Empty;
            if (objectFilters.Count > 0)
            {
                foreach (var item in objectFilters)
                {
                    // thêm điều kiện vào chuỗi where
                    // Kiểm tra toán tử
                    /// Nếu cột là string
                    if (item.ValueType == typeof(string).Name)
                    {
                        switch (item.Operator)
                        {
                            case Core.Enums.EnumOperator.Operator.Contain:
                                where += $" {item.Column} LIKE '%{item.Value}%' {item.AdditionalOperator}";
                                break;
                            case Core.Enums.EnumOperator.Operator.EqualTo:
                                where += $" {item.Column} = '{item.Value}' {item.AdditionalOperator}";
                                break;
                            case Core.Enums.EnumOperator.Operator.BeginWith:
                                where += $" {item.Column} LIKE '{item.Value}%' {item.AdditionalOperator}";
                                break;
                            case Core.Enums.EnumOperator.Operator.EndWith:
                                where += $" {item.Column} LIKE '%{item.Value}' {item.AdditionalOperator}";
                                break;
                            case Core.Enums.EnumOperator.Operator.NotContain:
                                where += $" {item.Column} NOT LIKE '%{item.Value}%' {item.AdditionalOperator}";
                                break;
                            default:
                                break;
                        }
                    }
                    else if (item.ValueType == typeof(int).Name)
                    {
                        switch (item.Operator)
                        {
                            case Core.Enums.EnumOperator.Operator.EqualTo:
                                where += $" {item.Column} = {item.Value} {item.AdditionalOperator}";
                                break;
                            case Core.Enums.EnumOperator.Operator.LessThan:
                                where += $" {item.Column} < {item.Value} {item.AdditionalOperator}";
                                break;
                            case Core.Enums.EnumOperator.Operator.LessThanOrEqualTo:
                                where += $" {item.Column} <= {item.Value} {item.AdditionalOperator}";
                                break;
                            case Core.Enums.EnumOperator.Operator.MoreThan:
                                where += $" {item.Column} > {item.Value} {item.AdditionalOperator}";
                                break;
                            case Core.Enums.EnumOperator.Operator.MoreThanOrEqualTo:
                                where += $" {item.Column} >= {item.Value} {item.AdditionalOperator}";
                                break;
                            default:
                                break;
                        }
                    }
                    // Cắt bỏ điều kiện AND/OR thừa ở cuối chuỗi
                    where = where.Substring(0, where.LastIndexOf(" "));
                }
            }
            return where;
        }
        /// <summary>
        /// Convert thành 1 câu lệnh điều kiện của ORDER BY
        /// </summary>
        /// <param name="objectSort">Đối tượng cần sẵp xếp</param>
        /// <returns>Câu lệnh where trong sql</returns>
        /// CreatedBy: TTKien(17/01/2022)
        private static string BuildSqlSort(ObjectSort objectSort)
        {
            string sort = string.Empty;
            if (objectSort != null)
            {
                switch (objectSort.SortOrder)
                {
                    case Core.Enums.EnumOperator.Sort.Asc:
                        sort = $"{objectSort.Column} {objectSort.SortOrder}";
                        break;
                    case Core.Enums.EnumOperator.Sort.Desc:
                        sort = $"{objectSort.Column} {objectSort.SortOrder}";
                        break;
                        ;
                    default:
                        break;
                }
            }
            return sort;
        }
        #endregion
    }
}
