using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.InMemoryStorage
{
    public class cInMemoryStorage : IPersistenceService
    {

        private readonly Dictionary<Type, List<BaseEntity>> _entityDictionary = new Dictionary<Type, List<BaseEntity>>();

        private List<BaseEntity> GetListOrCreateIfNotAvailable<T>() where T : BaseEntity
        {
            if (_entityDictionary.ContainsKey(typeof(T)))
            {
                return _entityDictionary[typeof(T)];
            }

            var newList = new List<BaseEntity>();
            _entityDictionary.Add(typeof(T), newList);

            return newList;
        }

        public Task<clsUser> AuthenticateAndGetUserAsync(clsUser objUser)
        {
            var list = GetListOrCreateIfNotAvailable<clsUser>();
            var foundUser = list.OfType<clsUser>()
                .FirstOrDefault(u => u.UserName == objUser.UserName && u.Password == objUser.Password);
            return Task.FromResult(foundUser);
        }

        public Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            var list = GetListOrCreateIfNotAvailable<T>();
            list.Remove(entity);
            Console.WriteLine("Delete - Id: " + entity.Id);
            Console.WriteLine(JsonSerializer.Serialize(entity));

            return Task.FromResult(true);
        }

        public Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseEntity
        {
            var list = GetListOrCreateIfNotAvailable<T>();
            var entityList = list.OfType<T>().Where(whereExpression.Compile());

            Console.WriteLine("Get - Count: " + entityList.Count());
            return Task.FromResult(entityList);
        }

        public Task InitAsync()
        {
            return Task.FromResult(true);
        }

        public Task<int> InsertAsync<T>(T entity) where T : BaseEntity
        {
            var list = GetListOrCreateIfNotAvailable<T>();
            var id = list.Count == 0 ? 1 : list.Max(e => e.Id) + 1;
            list.Add(entity);
            Console.WriteLine("Insert - Id: " + id);
            Console.WriteLine(JsonSerializer.Serialize(entity));

            return Task.FromResult(id);
        }

        public Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            Console.WriteLine("Update: " + entity.Id);
            Console.WriteLine(JsonSerializer.Serialize(entity));
            return Task.FromResult(true);
        }
    }
}
