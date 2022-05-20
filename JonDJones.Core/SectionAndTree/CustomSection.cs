using Umbraco.Cms.Core.Sections;

namespace JonDJones.Core.Section
{

    public class MyCustomSection : ISection
    {
        public string Alias => "myCustomSection";

        public string Name => "My Custom Section";
    }
}
