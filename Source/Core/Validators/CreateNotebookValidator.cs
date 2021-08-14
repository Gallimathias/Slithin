﻿using FluentValidation;
using Slithin.ViewModels.Modals;

namespace Slithin.Core.Validators
{
    public class CreateNotebookValidator : AbstractValidator<CreateNotebookModalViewModel>
    {
        public CreateNotebookValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Pages).Must(_ => _.Count > 0).WithMessage("A notebook has to have at least 1 page");
        }
    }
}
