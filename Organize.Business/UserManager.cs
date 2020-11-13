using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Organize.Business
{
    public class UserManager : IUserManager
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserManager(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<User> TrySignInAndGetUserAsync(User user)
        {
            return await _userDataAccess.AuthenticateAndGetUserAsync(user);
        }

        public async Task InserUserAsync(User user)
        {
            var isUserAlreadyAvailable = await _userDataAccess.IsUserWithNameAvailableAsync(user);
            if (isUserAlreadyAvailable)
            {
                throw new Exception("User name already exists");
            }

            await _userDataAccess.InsertUserAsync(user);
        }
    }
}
