using Gourd.Domain.Entity;
using Gourd.Domain.IRepository;
using Gourd.Infrastructure;
using Gourd.Infrastructure.config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.DomainCore.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public virtual DBConfig config { get; set; }
        public virtual DbSet<T> Table { get; set; }
        public virtual BaseDbContext DbContent { get; set; }

        public BaseRepository()
        {
   
        }

        public BaseRepository(BaseDbContext dbContext)
        {
            this.DbContent = dbContext;
            this.Table = this.DbContent.Set<T>();
        }
        #region 获取实例
        public virtual T Get(string id)
        {
            return this.Table.FirstOrDefault(f => f.Id == id);
        }

        public virtual T Get(Expression<Func<T, bool>> expression)
        {
            return this.Table.FirstOrDefault(expression);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Table.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<T> GetAsync(string id)
        {
            return await this.Table.FirstOrDefaultAsync(f => f.Id == id);
        }

        #endregion

        #region 获取列表
        public virtual List<T> GetList()
        {
            var list = Table.AsQueryable().ToList();
            return list;
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> expression)
        {
            return this.Table.Where(expression).ToList();
        }

        public virtual (List<T>, int) GetList(Expression<Func<T, bool>> expression, int? pageIndex, int? pageSize)
        {
            var query = this.Table.AsQueryable().Where(expression);
            var count =  query.Count();
            if (count == 0) return (new List<T>(), 0);
            //分页
            if (pageIndex.HasValue)
            {
                pageIndex = pageIndex.Value > 0 ? pageIndex : 1;
                pageSize = pageSize.Value > 0 ? pageSize : 15;
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return (query.ToList(), count);
        }

        public virtual async Task<List<T>> GetListAsync()
        {
            return await this.Table.ToListAsync();
        }

        public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Table.Where(expression).ToListAsync();
        }

        public virtual async Task<List<T>> GetListAsync(List<Expression<Func<T, bool>>> expression)
        {
            var query = this.Table.AsQueryable();
            if (expression != null)
            {
                expression.ForEach(m => { query = query.Where(m); });
            }
            return await query.ToListAsync();
        }

        public virtual async Task<(List<T>,int)> GetListAsync(Expression<Func<T, bool>> expression, int? pageIndex, int? pageSize)
        {
            var query = this.Table.AsQueryable().Where(expression);
            var count = await query.CountAsync();
            if (count == 0) return (new List<T>(), 0);
            //分页
            if (pageIndex.HasValue)
            {
                pageIndex = pageIndex.Value > 0 ? pageIndex : 1;
                pageSize = pageSize.Value > 0 ? pageSize : 15;
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return (await query.ToListAsync(), count);
        }

        public virtual async Task<(List<T>, int)> GetListAsync(List<Expression<Func<T, bool>>>expression, int? pageIndex, int? pageSize)
        {
            var query = this.Table.AsQueryable();
            if (expression != null)
            {
                expression.ForEach(m => { query = query.Where(m); });
            }
            var count = await query.CountAsync();
            if (count == 0) return (new List<T>(), 0);
            //分页
            if (pageIndex.HasValue)
            {
                pageIndex = pageIndex.Value > 0 ? pageIndex : 1;
                pageSize = pageSize.Value > 0 ? pageSize : 15;
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return (await query.ToListAsync(), count);
        }

        public async Task<(List<T>, int)> GetListAsync(Expression<Func<T, bool>> expression, int? pageIndex, int? pageSize, List<SortOrder> sortOrder)
        {
            var query = this.Table.AsQueryable().Where(expression);
            var count = await query.CountAsync();
            if (count == 0) return (new List<T>(), 0);
            //分页
            if (pageIndex.HasValue)
            {
                pageIndex = pageIndex.Value > 0 ? pageIndex : 1;
                pageSize = pageSize.Value > 0 ? pageSize : 15;
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            if (sortOrder == null) return (await query.ToListAsync(), count);
            if (sortOrder.Count() == 0) return (await query.ToListAsync(), count);

            //创建表达式变量参数
            var parameter = Expression.Parameter(typeof(T), sortOrder[0].value);
            //根据属性名获取属性
            var property = typeof(T).GetProperty(sortOrder[0].value);
            if (property != null)
            {
                //创建一个访问属性的表达式
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                string OrderName = sortOrder[0].orderType == OrderType.Desc ? "OrderByDescending" : "OrderBy";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                query = query.Provider.CreateQuery<T>(resultExp);
            }
            if (sortOrder.Count() > 1)
            {
                //创建表达式变量参数
                parameter = Expression.Parameter(typeof(T), sortOrder[1].value);
                //根据属性名获取属性
                property = typeof(T).GetProperty(sortOrder[1].value);
                if (property != null)
                {
                    //创建一个访问属性的表达式
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    string ThenBy = sortOrder[1].orderType == OrderType.Desc ? "ThenByDescending" : "ThenBy";
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), ThenBy, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                    query = query.Provider.CreateQuery<T>(resultExp);
                }
            }
            return (await query.ToListAsync(), count);
        }


        public async Task<(List<T>, int)> GetListAsync(List<Expression<Func<T, bool>>> expression, int? pageIndex, int? pageSize, List<SortOrder> sortOrder)
        {
            var query = this.Table.AsQueryable();
            if (expression != null)
            {
                expression.ForEach(m => { query = query.Where(m); });
            }
            var count = await query.CountAsync();
            if (count == 0) return (new List<T>(), 0);
            //分页
            if (pageIndex.HasValue)
            {
                pageIndex = pageIndex.Value > 0 ? pageIndex : 1;
                pageSize = pageSize.Value> 0 ? pageSize : 15;
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            if (sortOrder == null) return (await query.ToListAsync(), count);
            if (sortOrder.Count() == 0) return (await query.ToListAsync(), count);

            //创建表达式变量参数
            var parameter = Expression.Parameter(typeof(T), sortOrder[0].value);
            //根据属性名获取属性
            var property = typeof(T).GetProperty(sortOrder[0].value);
            if (property != null)
            {
                //创建一个访问属性的表达式
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                string OrderName = sortOrder[0].orderType == OrderType.Desc ? "OrderByDescending" : "OrderBy";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                query = query.Provider.CreateQuery<T>(resultExp);
            }
            if (sortOrder.Count() > 1)
            {
                //创建表达式变量参数
                parameter = Expression.Parameter(typeof(T), sortOrder[1].value);
                //根据属性名获取属性
                property = typeof(T).GetProperty(sortOrder[1].value);
                if (property != null)
                {
                    //创建一个访问属性的表达式
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    string ThenBy = sortOrder[1].orderType == OrderType.Desc ? "ThenByDescending" : "ThenBy";
                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), ThenBy, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                    query = query.Provider.CreateQuery<T>(resultExp);
                }
            }
            return (await query.ToListAsync(), count);
        }

        #endregion

        #region 添加实体
        public virtual T Add(T entity)
        {
            var result = this.Table.Add(entity).Entity;
            this.DbContent.SaveChanges();
            return result;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var result = this.Table.Add(entity).Entity;
            await this.DbContent.SaveChangesAsync();
            return result;
        }

        public virtual void Add(IList<T> entities)
        {
            this.Table.AddRange(entities);
            this.DbContent.SaveChanges();
        }

        public virtual async Task AddAsync(IList<T> entities)
        {
            this.Table.AddRange(entities);
            await this.DbContent.SaveChangesAsync();
        }

        #endregion

        #region 更新实体
        public virtual void Update(T entity)
        {
            this.DbContent.Update(entity);
            this.DbContent.SaveChanges();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            this.DbContent.Update(entity);
            await this.DbContent.SaveChangesAsync();
        }

        public virtual void Update(IList<T> entities)
        {
            this.DbContent.UpdateRange(entities);
            this.DbContent.SaveChanges();
        }

        public virtual async Task UpdateAsync(IList<T> entities)
        {
            this.DbContent.UpdateRange(entities);
            await this.DbContent.SaveChangesAsync();
        }
        #endregion

        #region 删除实体
        public virtual void Delete(string id)
        {
            var entity = this.Table.First(f => f.Id == id);
            this.DbContent.Remove(entity);
            this.DbContent.SaveChanges();
        }

        public virtual void Delete(IList<string> ids)
        {
            var entities = this.Table.Where(f => ids.Contains(f.Id));
            this.DbContent.RemoveRange(entities);
            this.DbContent.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            this.DbContent.Remove(entity);
            this.DbContent.SaveChanges();
        }

        public virtual void Delete(IList<T> entities)
        {
            this.DbContent.RemoveRange(entities);
            this.DbContent.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> expression)
        {
            var entity = this.Table.Where(expression);
            this.DbContent.Remove(entity);
            this.DbContent.SaveChanges();
        }

        public virtual async Task DeleteAsync(string id)
        {
            var entity = await this.Table.FirstAsync(f => f.Id == id);
            this.DbContent.Remove(entity);
            await this.DbContent.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(IList<string> ids)
        {
            var entities = this.Table.Where(f => ids.Contains(f.Id));
            this.DbContent.RemoveRange(entities);
            await this.DbContent.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            this.DbContent.Remove(entity);
            await this.DbContent.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(IList<T> entities)
        {
            this.DbContent.RemoveRange(entities);
            await this.DbContent.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = this.Table.Where(expression);
            this.DbContent.Remove(entity);
            await this.DbContent.SaveChangesAsync();
        }

        #endregion

        #region 校验数据
        public virtual bool IsExist(Expression<Func<T, bool>> expression)
        {
            return this.Table.Any(expression);
        }

        public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Table.AnyAsync(expression);
        }

        public virtual int Count()
        {
            return this.Table.Count();
        }

        public virtual  int Count(Expression<Func<T, bool>> expression)
        {
            return this.Table.Count(expression);
        }

        public virtual async Task<int> CountAsync()
        {
            return await this.Table.CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await this.Table.CountAsync(expression);
        }
        #endregion
    }
}
