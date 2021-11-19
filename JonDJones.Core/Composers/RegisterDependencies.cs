using JonDJones.Core.Interfaces;
using JonDJones.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
