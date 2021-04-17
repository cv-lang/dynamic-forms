using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.PropertyGrid
{
    public class PropertyGridNodeModel : PageModel
    {
        private readonly DataServiceBase dataService;
        private readonly ViewService viewService;

        public Model.PropertyBaseVM PropertyGrid { get; set; }

        public PropertyGridNodeModel(DataServiceBase dataService, ViewService viewService)
        {
            this.dataService = dataService;
            this.viewService = viewService;
        }

        public void OnGet()
        {
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var type = query["type"];
            var bindingPath = query["bindingPath"];

            if(string.IsNullOrEmpty(objectIdStr))
            {
                return;
            }

            PropertyGrid = viewService.GetViewModel(objectIdStr, type, bindingPath);
        }
    }
}
