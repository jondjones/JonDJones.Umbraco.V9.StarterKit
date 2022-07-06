using System;

namespace JonDJones.Core.ViewModel.Poco
{
    public class LinkPoco
    {
        public int UmbracoId { get; set; }

        public string Category { get; set; }

        public string Url { get; set; }

        public DateTime? PostDate { get; set; }

        public string Name { get; set; }

        public string Culture { get; set; }

        public string LastPublishedBy { get; set; }

        public int DaysTillPublished { get; set; }

        public string PostDateAsString { get; set; }
    }
}
