using JonDJones.Core.Interfaces;
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