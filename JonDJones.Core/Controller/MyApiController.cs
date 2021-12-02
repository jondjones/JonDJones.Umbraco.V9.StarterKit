using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Controllers;

namespace JonDJones.Core.Controller
{
    /// <summary>
    /// ~/Umbraco/Api/MyApi/GetSomeData
    /// </summary>
    public class MyApiController : UmbracoApiController
    {
        public string GetSomeData()
        {
            return "This is API Data";
        }
    }
}