using SortingApp_Desktop.Common.api.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SortingApp_Desktop.Common.@base.Interfaces
{
    public interface IBaseRepos<TModel>
    {
        /// <summary>
        /// GetOpenConnection
        /// </summary>
        /// <returns></returns>
        IDbConnection GetOpenConnection();
        /// <summary>
        /// Phân trang tìm kiếm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<MethodResult<IEnumerable<TModel>>> GetsBySearch(BaseSearchModel model);

        /// <summary>
        /// Lấy tất cả dữ liệu trong bảng
        /// </summary>
        /// <returns></returns>
        Task<MethodResult<IEnumerable<TModel>>> GetsAll();

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MethodResult<TModel>> GetById(long id);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MethodResult<long> Insert(TModel model);

        /// <summary>
        /// InsertMany
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        MethodResult InsertMany(List<TModel> models);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<MethodResult> Delete(long id, long userId);
        /// <summary>
        /// DeleteMany
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<MethodResult> DeleteMany(string ids, long userId);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MethodResult Update(TModel model);
    }
}
