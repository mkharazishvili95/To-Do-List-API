using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_API.Models;
using To_Do_List_API.Services;

namespace To_Do_List_API_Tests.FakeServices
{
    public class FakeUserService : IUserService
    {
        private readonly List<User> _users;

        public FakeUserService(List<User> users)
        {
            _users = users;
        }

        public User GetUserById(int userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<User> GetOwnBalance()
        {
            return _users;
        }

        public User Login(LoginModel loginModel)
        {
            return _users.FirstOrDefault(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password);
        }

        public bool MakeTransfer(int senderId, int receiverId, double amount)
        {
            return true;
        }

        public User Register(RegisterModel registerModel)
        {
            if (string.IsNullOrEmpty(registerModel.UserName) || string.IsNullOrEmpty(registerModel.Password))
            {
                return null;
            }

            var newUser = new User
            {
                Id = _users.Count + 1,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.UserName,
                Password = registerModel.Password,
            };
            _users.Add(newUser);
            return newUser;
        }
    }
}
