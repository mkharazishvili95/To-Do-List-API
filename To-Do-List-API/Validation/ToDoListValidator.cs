using FluentValidation;
using To_Do_List_API.Models;
using static To_Do_List_API.Models.ToDoListModel;

namespace To_Do_List_API.Validation
{
    public class ToDoListValidator : AbstractValidator<ToDoListModel>
    {
        public ToDoListValidator()
        {
            RuleFor(addToDoList => addToDoList.Title).NotEmpty().WithMessage("Enter To-Do List Title!");
            RuleFor(addToDoList => addToDoList.Description).NotEmpty().WithMessage("Enter To-Do List Description!");
            RuleFor(addToDoList => addToDoList.FinalDate).NotEmpty().WithMessage("Enter To-Do List Final date!");
            RuleFor(addToDoList => addToDoList.Category).NotEmpty().WithMessage("Enter To-Do List Type!")
                .Must(BeAValidCategory).WithMessage("Invalid Category! Must be 'Home', 'Work', 'Travel', 'Personal', 'Education' or 'Other' ");
        }
        private bool BeAValidCategory(string category)
        {
            return category == "Home" || category == "Work" || category == "Personal" || category == "Education" || category == "Travel" || category == "Other";
        }
    }
}
