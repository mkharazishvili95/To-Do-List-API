using System.Linq;
using To_Do_List_API.Data;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;
using To_Do_List_API.Validation;

namespace To_Do_List_API.Services
{
    public interface IUserService
    {
        User Register(RegisterModel registerModel);
        User Login(LoginModel loginModel);
    }
    public class UserService : IUserService
    {
        private readonly ToDoListContext _context;
        public UserService(ToDoListContext context)
        {
            _context = context;
        }

        public User Login(LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return null;
            }
            var user = _context.Users.FirstOrDefault(x => x.UserName == loginModel.UserName);
            if (user == null)
            {
                return null;
            }
            if (HashSettings.HashPassword(loginModel.Password) != user.Password)
            {
                return null;
            }
            _context.SaveChanges();
            return user;
        }

        public User Register(RegisterModel registerModel)
        {
            var validator = new NewUserValidator(_context);
            var validatorResult = validator.Validate(registerModel);
            if (!validatorResult.IsValid)
            {
                return null;
            }
            else
            {
                var newUser = new User
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.UserName,
                    Password = HashSettings.HashPassword(registerModel.Password),
                };
                _context.Add(newUser);
                _context.SaveChanges();
                return (newUser);
            }
        }
    }
}
