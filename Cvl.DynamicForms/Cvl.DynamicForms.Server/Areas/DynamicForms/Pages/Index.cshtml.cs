using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cvl.DynamicForms.Base;
using Cvl.DynamicForms.Services;
using Cvl.DynamicForms.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages
{
    public class UrlLink
    {
        public string Type { get; internal set; }
        public string Url { get; internal set; }
    }

    public class IndexModel : PageModelBase
    {
        private readonly ViewConfigurationService viewConfigurationService;

        public IndexModel(ViewConfigurationService viewConfigurationService, ApplicationConfigurtion applicationConfigurtion) :base(applicationConfigurtion)
        {
            this.viewConfigurationService = viewConfigurationService;
        }

        public List<UrlLink> Types { get; set; }

        public void OnGet()
        {
            Types = new List<UrlLink>();
            var types = viewConfigurationService.GetTypes();
            foreach (var item in types.OrderByDescending(x=> x.IsFavourite).ThenBy(x=> x.FullTypeName))
            {
                var l = new UrlLink();
                l.Type = item.FullTypeName;
                l.Url = $"{ApplicationUrl}/DynamicForms/Grid?type={item.FullTypeName}";
                Types.Add(l);
            }
        }
    }
}
