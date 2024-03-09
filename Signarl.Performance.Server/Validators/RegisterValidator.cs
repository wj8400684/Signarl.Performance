using FastEndpoints;
using FluentValidation;
using Signarl.Performance.Core.Models;

namespace Signarl.Performance.Server.Validators;

public sealed class RegisterValidator : Validator<UserRegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("账号或密码不能为空!")
            .MinimumLength(5)
            .WithMessage("账号不能太短");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("账号或密码不能为空!")
            .MinimumLength(6)
            .WithMessage("密码不能太短!");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("邮箱地址不能为空!")
            .MinimumLength(6)
            .WithMessage("邮箱地址不能太短!");
    }
}