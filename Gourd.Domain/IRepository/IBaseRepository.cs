using Gourd.Domain.Entity;
using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gourd.Domain.IRepository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        #region 获取实例
        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>

        T Get(string id);
        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取单条数据信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(string id);

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取所有列表信息
        /// </summary>
        /// <returns></returns>
        List<T> GetList();

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression">表达式条件</param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression">表达式条件</param>
        /// <returns></returns>
        (List<T>,int) GetList(Expression<Func<T, bool>> expression, int? pageIndex, int? pageSize);


        /// <summary>
        /// 获取所有列表信息
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync();

        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression">表达式条件</param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetListAsync(List<Expression<Func<T, bool>>> expression);
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression">表达式条件</param>
        /// <returns></returns>
        Task<(List<T>,int)> GetListAsync(Expression<Func<T, bool>> expression, int? pageIndex, int? pageSize);


        Task<(List<T>, int)> GetListAsync(List<Expression<Func<T, bool>>> expression, int? pageIndex, int? pageSize);
        /// <summary>
        /// 获取列表信息
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        Task<(List<T>,int)> GetListAsync(Expression<Func<T, bool>> expression, int? pageIndex, int? pageSize, List<SortOrder> sortOrder);

        Task<(List<T>, int)> GetListAsync(List<Expression<Func<T, bool>>> expression, int? pageIndex, int? pageSize, List<SortOrder> sortOrder);
        #endregion

        #region 添加实体
        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <param name="entity">实体</param>
        T Add(T entity);

        /// <summary>
        /// 添加实体信息
        /// </summary>
        /// <param name="entity">实体</param>
        Task<T> AddAsync(T entity);


        /// <summary>
        /// 批量添加实体信息
        /// </summary>
        /// <param name="entities">实体</param>
        void Add(IList<T> entities);

        /// <summary>
        /// 批量添加实体信息
        /// </summary>
        Task AddAsync(IList<T> entities);
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(T entity);

        /// <summary>
        /// 修改数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// 批量修改实体信息
        /// </summary>
        /// <param name="entities">实体</param>
        void Update(IList<T> entities);


        /// <summary>
        /// 批量修改实体信息
        /// </summary>
        /// <param name="entities">实体</param>
        Task UpdateAsync(IList<T> entities);
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="id">主键</param>
        void Delete(string id);

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="ids">主键</param>
        void Delete(IList<string> ids);

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(T entity);

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(IList<T> entities);

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="id">主键</param>
        void Delete(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="id">主键</param>
        Task DeleteAsync(string id);

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="ids">主键</param>
        Task DeleteAsync(IList<string> ids);

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="entity">实体</param>
        Task DeleteAsync(IList<T> entities);

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="id">主键</param>
        Task DeleteAsync(Expression<Func<T, bool>> expression);

        #endregion

        #region 校验数据
        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        bool IsExist(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 数据总数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 数据总数
        /// </summary>
        /// <param name="expression">过滤条件</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 数据总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
        /// <summary>
        /// 数据总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        #endregion

    }
}
