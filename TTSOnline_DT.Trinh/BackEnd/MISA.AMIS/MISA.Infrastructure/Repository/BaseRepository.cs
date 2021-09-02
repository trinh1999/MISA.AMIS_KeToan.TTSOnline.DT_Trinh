using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructor.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : BaseEntity
    {
        //Truy cập vào database
        //1. Khởi tạo thông tin kết nối database
        #region DECLARE

        /// <summary>
        /// Kết nối tới DB
        /// </summary>
        protected IDbConnection _dbConnection;

        /// <summary>
        /// Thông tin kết nối 
        /// </summary>
        public readonly string _connectionString;

        /// <summary>
        /// Tên bảng
        /// </summary>
        protected string _tableName;
        #endregion

        #region Contructor
        public BaseRepository(IConfiguration configuration)
        {
            _tableName = typeof(TEntity).Name;

            _connectionString = configuration.GetConnectionString("AmisDatabase");

            
        }

        #endregion
        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// <returns>Trả về số bản ghi được thêm</returns>
        /// CreateBy: DT.Trinh
        public int Add(TEntity entity)
        {
            var rowEffects = 0;

            _dbConnection = new MySqlConnection(_connectionString);
            _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var parameters = MappingDbType(entity);

                    rowEffects = _dbConnection.Execute($"Proc_Insert{_tableName}", param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return rowEffects;
        }

        /// <summary>
        /// Xóa bản ghi     
        /// </summary>
        /// <param name="entityId">Bản ghi cần xóa</param>
        /// <returns>Số bản ghi đã xóa</returns>
        ///  CreateBy: DT.Trinh
        public int Delete(Guid entityId)
        {
            _dbConnection = new MySqlConnection(_connectionString);

            _dbConnection.Open();

            var rowEffects = 0;

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var parameters = new DynamicParameters();

                    parameters.Add($"@{_tableName}Id", entityId);

                    rowEffects = _dbConnection.Execute($"Proc_Delete{_tableName}ById", param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
          
            return rowEffects;
        }

        /// <summary>
        /// Hàm get ra danh sách
        /// </summary>
        /// <returns>List danh sách</returns>
        ///  CreateBy: DT.Trinh
        public IEnumerable<TEntity> Get()
        {
            _dbConnection = new MySqlConnection(_connectionString);

            _dbConnection.Open();

            var sql = $"Proc_Get{_tableName}s";

            var entities = _dbConnection.Query<TEntity>(sql, commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Lọc bản ghi theo tên thuộc tính và giá trị thuộc tính 
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="property">Tên thuộc tính</param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public TEntity GetByProp(TEntity entity, PropertyInfo property)
        {
            _dbConnection = new MySqlConnection(_connectionString);

            _dbConnection.Open();

            var propName = property.Name;

            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);

            var propValue = entity.GetType().GetProperty(propName).GetValue(entity);

            var query = string.Empty;

            if (entity.EntityState == EntityState.AddNew)
            {
                query = $"SELECT * FROM {_tableName} WHERE {propName} = '{propValue}'";
            }
            else if (entity.EntityState == EntityState.Update)
            {
                query = $"SELECT * FROM {_tableName} WHERE {propName} = '{propValue}' AND {_tableName}Id <> '{keyValue}'";

            }
            else
            {
                return null;
            }
            var res = _dbConnection.QueryFirstOrDefault<TEntity>(query, commandType: CommandType.Text);
            return res;
        }
        /// <summary>
        /// Lọc theo id bản ghi
        /// </summary>
        /// <param name="entityId">id bản ghi</param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        public TEntity GetById(Guid entityId)
        {
            _dbConnection = new MySqlConnection(_connectionString);

            _dbConnection.Open();

            var parameters = new DynamicParameters();

            parameters.Add($"@{_tableName}Id", entityId);

            var res = _dbConnection.Query<TEntity>($"Proc_Get{_tableName}ById", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return res;
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: DT.Trinh
        public int Update(TEntity entity, Guid entityId)
        {
            var rowEffects = 0;

            _dbConnection = new MySqlConnection(_connectionString);

            _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var parameters = MappingDbType(entity);

                    rowEffects = _dbConnection.Execute($"Proc_Update{_tableName}", param: parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return rowEffects;
        }

        /// <summary>
        /// Map dữ liệu của 1 entity sang thành dynamic parameters dùng cho truy vấn SQL
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns>dynamic parameters đã được format đúng</returns>
        /// CreatedBy: DT.Trinh
        protected DynamicParameters MappingDbType(TEntity entity)
        {
            var properties = entity.GetType().GetProperties();

            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                var propertyValue = property.GetValue(entity);

                var propertyType = property.PropertyType;

                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            return parameters;
        }

        /// <summary>
        /// Close kết nối
        /// </summary>
        ///  CreateBy: DT.Trinh
        public void Dispose()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }


    }
}
