using Microsoft.AspNetCore.Html;
using System;

namespace JonDJones.Core.ViewModel.Component
{
    public class BlogItemViewModel
    {
        public HtmlString Content { get; set; }

        public string ImageUrl { get; set; }

        public string LinkUrl { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Id { get; set; }
    }
}
