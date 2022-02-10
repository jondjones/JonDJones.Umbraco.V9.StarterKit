using JonDJones.Core.ViewModel.Component;
using System.Collections.Generic;

namespace JonDJones.Core.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<BlogItemViewModel> Blogs { get; set; }
    }
}
