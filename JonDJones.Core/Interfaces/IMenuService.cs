using System.Collections.Generic;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Services
{
    public interface IMenuService
    {
        IEnumerable<MenuItem> Menus { get; }
    }
}