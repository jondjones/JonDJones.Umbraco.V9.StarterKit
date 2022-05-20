namespace JonDJones.Core.Controller.Vanilla
{
    public class AdditionalLinkViewModel
    {
        public AdditionalLinkViewModel()
        {
            PublishedStatus = "Published";
        }

        public string LinkUrl { get; set; }

        public string PublishedStatus { get; set; }

        public string BitlyAPIKey { get; set; }
    }
}