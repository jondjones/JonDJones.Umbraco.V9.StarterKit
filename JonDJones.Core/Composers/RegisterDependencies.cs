using JonDJones.Core.Interfaces;
using JonDJones.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace JonDJones.Core.Composers
{
    public class RegisterDependencies : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IBlogService, BlogService>();
            builder.Services.AddTransient<IMenuService, MenuService>();
            builder.Services.AddTransient<ISettingsService, SettingsService>();
        }
    }
}