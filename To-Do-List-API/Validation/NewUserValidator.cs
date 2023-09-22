using FluentValidation;
using System.Linq;
using To_Do_List_API.Data;
using To_Do_List_API.Models;

namespace To_Do_List_API.Validation
{
    public class NewUserValidator : AbstractValidator<RegisterModel>
    {
        private readonly ToDoListContext _context;
        public NewUserValidator(ToDoListContext toDoListContext) {
            _context = toDoListContext;
            RuleFor(newUser => newUser.FirstName).NotEmpty().WithMessage("Enter your FirstName!");
            RuleFor(newUser => newUser.LastName).NotEmpty().WithMessage("Enter your LastName!");
            RuleFor(newUser => newUser.UserName).NotEmpty().WithMessage("Enter your UserName!")
                .Length(5, 20).WithMessage("UserName length must be between 5 and 20 chars!")
                .Must(differentUserName).WithMessage("UserName already exists!");
            RuleFor(newUser => newUser.Password).NotEmpty().WithMessage("Enter your Password!")
                .Length(5, 20).WithMessage("Password length must be between 5 and 20 chars!");
        }
        private bool differentUserName(string userName)
        {
            var differentName = _context.Users.SingleOrDefault(existingUser => existingUser.UserName.ToUpper() == userName.ToUpper());
            return differentName == null;
        }
    }
}
