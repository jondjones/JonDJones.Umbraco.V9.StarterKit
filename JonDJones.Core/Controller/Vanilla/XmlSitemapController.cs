using JonDJones.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JonDJones.Core.Controller.Vanilla
{
    public class XmlSitemapController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IXmlSitemapService _xmlSitemapService;

        public XmlSitemapController(
            IXmlSitemapService xmlSitemapService)
        {
            _xmlSitemapService = xmlSitemapService;
        }

        public ViewResult Index()
        {
            var viewModel = _xmlSitemapService.GetXmlSitemapViewModel();
            return View(viewModel);
        }
    }
}
