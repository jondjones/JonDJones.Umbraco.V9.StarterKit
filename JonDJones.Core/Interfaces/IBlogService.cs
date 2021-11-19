using JonDJones.Core.ViewModel.Component;
using System.Collections.Generic;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace JonDJones.Core.Interfaces
{
    public interface IBlogService
    {
        public IEnumerable<BlogItemViewModel> Blogs { get; }
    }
}
