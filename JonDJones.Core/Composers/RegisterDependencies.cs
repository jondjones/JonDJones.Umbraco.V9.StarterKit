using JonDJones.Core.Interfaces;
using JonDJones.Core.Interfaces.Example;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace JonDJones.Core.Composers
{
    public class RegisterDependencies : IComposer
    {
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<ITransient, GetId>();

        builder.Services.AddScoped<IScoped, GetId>();

        builder.Services.AddSingleton<ISingleton, GetId>();

        /// Use Scrutor to auto discover and auto wire-up configuration
        builder.Services.Scan(scan => scan
                .FromAssemblies(
                typeof(IBlogService).Assembly
                )
                .AddClasses()
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}