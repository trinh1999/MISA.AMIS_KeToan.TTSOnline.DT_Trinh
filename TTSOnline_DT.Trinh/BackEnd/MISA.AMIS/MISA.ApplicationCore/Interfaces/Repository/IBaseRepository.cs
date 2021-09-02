using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        #region METHOD
        /// <summary>
        /// Lấy all dữ liệu
        /// </summary>
        /// <returns>list dl</returns>
        /// CreateBy DT.Trinh 
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Thực thể TEntity</param>
        /// <returns>Số bản ghi được thêm </returns>
        /// CreateBy DT.Trinh 
        int Add(TEntity entity);

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entity">Thực thể TEntity</param>
        /// <param name="entityId">Khóa chính của thực thể</param>
        /// <returns>Số bản ghi đã sửa</returns>
        /// CreateBy DT.Trinh 
        int Update(TEntity entity, Guid entityId);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// CreateBy DT.Trinh 
        int Delete(Guid entityId);

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId">Id bản ghi</param>
        /// <returns>Bản ghi có id tương ứng</returns>
        /// CreateBy DT.Trinh 
        TEntity GetById(Guid entityId);

        /// <summary>
        /// Lọc bản ghi
        /// </summary>
        /// <param name="entity">Thực thể kiểu TEntity</param>
        /// <param name="property">Thuộc tính</param>
        /// <returns>Bản ghi tương ứng</returns>
        /// CreateBy DT.Trinh 
        TEntity GetByProp(TEntity entity, PropertyInfo property);
        #endregion
    }
}
