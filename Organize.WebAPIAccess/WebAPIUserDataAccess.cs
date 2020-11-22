﻿using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organize.WebAPIAccess
{
    public class WebAPIUserDataAccess : IUserDataAccess
    {
        private WebAPIAccess _webAPIAccess;

        public WebAPIUserDataAccess(IPersistanceService persistanceService)
        {
            _webAPIAccess = (WebAPIAccess)persistanceService;
        }

        public async Task<User> AuthenticateAndGetUserAsync(User user)
        {
            return await _webAPIAccess.AuthenticateAndGetUserAsync(user);
        }

        public async Task InsertUserAsync(User user)
        {
            await _webAPIAccess.InsertAsync(user);
        }

        public Task<bool> IsUserWithNameAvailableAsync(User user)
        {
            return Task.FromResult(false);
        }
    }
}
