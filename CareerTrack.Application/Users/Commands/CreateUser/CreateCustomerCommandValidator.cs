using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Id).Length(5).NotEmpty();
            //add other rules here
        }
    }
}
