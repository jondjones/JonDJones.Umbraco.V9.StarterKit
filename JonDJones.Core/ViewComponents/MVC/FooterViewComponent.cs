using JonDJones.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JonDJones.Core.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private ISettingsService _settings;

        public FooterViewComponent(
                ISettingsService settings)
        {
            _settings = settings;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footer = new FooterViewModel
            {
                ComponentArea = _settings.SettingsPage.ComponentArea
            };

            return View(footer);
        }
    }
}
