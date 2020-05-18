using Sparkle.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sparkle.Domain.Services
{
    public class FriendService
    {
        #region Private Members

        private readonly UserService _userService;

        #endregion

        #region Constructor

        public FriendService(UserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Public Methods

        #region Sync

        /// <summary>
        /// Add <paramref name="friend"/> to <paramref name="user"/>
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="friend">Friend</param>
        public void Add(User user, Friend friend)
        {
            if (user.Friends as List<Friend> == null)
            {
                user.Friends = new List<Friend>();
            }
            ((List<Friend>)user.Friends).Add(friend);
            _userService.Update(user);
        }

        /// <summary>
        /// Remove <paramref name="friend"/> from <paramref name="user"/>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="friend"></param>
        public void Remove(User user, Friend friend)
        {
            if (user.Friends as List<Friend> == null)
            {
                return;
            }
            ((List<Friend>)user.Friends).Remove(friend);
            _userService.Update(user);
        }

        #endregion

        #region Async
        /// <summary>
        /// Add a new <paramref name="friend"/> to <paramref name="user"/>
        /// </summary>
        /// <param name="user">User which will be contain new <paramref name="friend"/></param>
        /// <param name="friend">Friend</param>
        public Task AddAsync(User user, Friend friend)
        {
            if (user.Friends as List<Friend> == null)
            {
                user.Friends = new List<Friend>();
            }
            ((List<Friend>)user.Friends).Add(friend);

            return _userService.UpdateAsync(user);
        }

        /// <summary>
        /// Remove <paramref name="friend"/> from <paramref name="user"/>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="friend"></param>
        public Task RemoveAsync(User user, Friend friend)
        {
            if (user.Friends as List<Friend> == null)
            {
                return new Task(() => { return; });
            }
            ((List<Friend>)user.Friends).Remove(friend);
            return _userService.UpdateAsync(user);
        }

        /// <summary>
        /// Add a new friend with <paramref name="newFriendId"/> to <paramref name="user"/>
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="newFriendId">New friend <see cref="User.Id"/></param>
        /// <returns></returns>
        public async Task AddAsync(ClaimsPrincipal user, string newFriendId)
        {
            User friendUser = await _userService.GetAsync(newFriendId);
            var currentUser = await _userService.GetAsync(user);
            var friend = new Friend()
            {
                FriendId = newFriendId,
                Name = friendUser.Name,
                Surname = friendUser.Surname,
                Username = friendUser.UserName
            };

            await AddAsync(currentUser, friend);
            return;
        }
        #endregion
        #endregion
    }
}
