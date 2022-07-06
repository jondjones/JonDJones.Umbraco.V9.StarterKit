using JonDJones.Core.ViewModel.Poco;
using System.Collections.Generic;

namespace JonDJones.Core.ViewModel.Page
{
    public class BlogViewModel
    {
        public string TranslationText { get; internal set; }
        public List<LinkPoco> LanguagePickerLinks { get; internal set; }
    }
}