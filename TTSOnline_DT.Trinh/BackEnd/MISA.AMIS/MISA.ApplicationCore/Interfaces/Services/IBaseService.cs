using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        #region Method

        /// <summary>
        /// Láy all dữ liệu 
        /// </summary>
        /// <returns>list dữ liệu </returns>
        /// CreateBy DT.Trinh 
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Thực thể TEntity </param>
        /// <returns>Thông tin trả về</returns>
        /// CreateBy DT.Trinh 
        ServiceResult Add(TEntity entity);

        /// <summary>
        /// Edit bản ghi
        /// </summary>
        /// <param name="entity">Thực thể TEntity </param>
        /// <param name="entityId">id </param>
        /// <returns>Thông tin trả về </returns>
        /// CreateBy DT.Trinh 
        ServiceResult Update(TEntity entity, Guid entityId);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityId">id </param>
        /// <returns>Thông tin trả về </returns>
        /// CreateBy DT.Trinh 
        /// 
        ServiceResult Delete(Guid entityId);

        /// <summary>
        /// Lấy dữ liệu qua id
        /// </summary>
        /// <param name="entityId">id </param>
        /// <returns>Bản ghi có id tương ứng </returns>
        /// CreateBy DT.Trinh 
        TEntity GetById(Guid entityId);

        /// <summary>
        /// Láy dữ liệu theo thuộc tính
        /// </summary>
        /// <param name="entity">Thực thể kiểu TEntity</param>
        /// <param name="property">THuộc tính</param>
        /// <returns>Bản ghi có thuộc tính tương ứng</returns>
        /// CreateBy DT.Trinh 
        TEntity GetByProp(TEntity entity, PropertyInfo property);

        #endregion
    }
}
