using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JonDJones.Core.Services
{
    public static class StringHelper
    {
        public static readonly int ContentCharacterLimit = 250;

        private const string HtmlTagPattern = "<.*?>";
        private const string PreviewColon = "...";

        public static string CreatePreviewText(string inputString)
        {
            return inputString.Substring(0, Math.Min(inputString.Length, ContentCharacterLimit)) + PreviewColon;
        }

        public static string StripHtml(string inputString)
        {
            return Regex.Replace(inputString, HtmlTagPattern, string.Empty);
        }

    }
}
