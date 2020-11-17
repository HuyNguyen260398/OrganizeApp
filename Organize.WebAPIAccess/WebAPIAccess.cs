using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Organize.WebAPIAccess
{
    public class WebAPIAccess : IPersistanceService
    {
        private readonly HttpClient _httpClient;

        public WebAPIAccess(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<User> AuthenticateAndGetUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteteAsync<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> whereExpression) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task InitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
