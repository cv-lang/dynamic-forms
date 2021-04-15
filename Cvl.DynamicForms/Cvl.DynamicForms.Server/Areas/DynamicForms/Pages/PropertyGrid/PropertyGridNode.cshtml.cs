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
        private readonly PropretyGridService viewService;

        public Model.PropertyGridVM PropertyGrid { get; set; }

        public PropertyGridNodeModel(DataServiceBase dataService, ViewConfigurationService viewConfigurationService)
        {
            this.dataService = dataService;
            this.viewService = new PropretyGridService(dataService, viewConfigurationService);
        }

        public void OnGet()
        {
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var type = query["type"];
            var bindingPath = query["bindingPath"];

            var parameters = new Parameters(query.Select(x => new Parameter() { Key = x.Key, Value = x.Value.ToString() }));

            PropertyGrid = viewService.GetPropertyGrid(objectIdStr, type, parameters, bindingPath);
        }
    }
}
