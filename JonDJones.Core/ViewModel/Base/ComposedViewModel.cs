using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JonDJones.Core.ViewModel.Base
{
    public class ComposedPageViewModel<TPage, TViewModel>
           where TPage : PublishedContentModel
    {
        public TPage Page { get; set; }

        public TViewModel ViewModel { get; set; }
    }
}
