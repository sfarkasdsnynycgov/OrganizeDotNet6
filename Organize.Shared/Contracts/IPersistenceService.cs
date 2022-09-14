using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Shared.Contracts
{
    public interface IPersistenceService
    {
        Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseEntity;
        Task<int> InsertAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
        Task InitAsync();
        Task<clsUser> AuthenticateAndGetUserAsync(clsUser objUser);
    }
}
