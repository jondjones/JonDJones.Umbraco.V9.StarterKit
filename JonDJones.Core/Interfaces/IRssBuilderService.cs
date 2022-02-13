using System.ServiceModel.Syndication;

namespace JonDJones.Core.Interfaces
{
    public interface IRssBuilderService
    {
        SyndicationFeed CreateRssFeed(
            string feedName,
            string description,
            string feedUrl,
            string authorEmail);
    }
}
