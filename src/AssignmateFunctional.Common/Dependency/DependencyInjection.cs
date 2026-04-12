using AssignmateFunctional.Common.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace AssignmateFunctional.Common.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCommonPackage(this IServiceCollection services)
    {
        _ = services.AddFluentValidationAutoValidation();
        _ = services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

        return services;
    }
}
