using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Application.Validator
{
    internal class BaseValidator
    {
    }

    public class CreateTodoListCommandValidator : AbstractValidator<BaseQuery<TodoItem>>
    {

        public CreateTodoListCommandValidator()
        {

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        }

        //public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        //{
        //    return await _context.TodoLists
        //        .AllAsync(l => l.Title != title);
        //}
    }
}
