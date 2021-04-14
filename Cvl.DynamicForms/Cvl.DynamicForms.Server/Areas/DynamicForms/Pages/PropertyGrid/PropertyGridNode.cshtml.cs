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

        public Model.PropertyGridViewModel PropertyGrid { get; set; }

        public PropertyGridNodeModel(DataServiceBase dataService, ViewConfigurationService viewConfigurationService)
        {
            this.dataService = dataService;
            this.viewService = new PropretyGridService(dataService, viewConfigurationService);
        }

        public void OnGet()
        {
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var objectId = long.Parse(objectIdStr);
            var type = query["type"];
            var bindingPath = query["bindingPath"].ToString();

            var parameters = new Parameters(query.Select(x => new Parameter() { Key = x.Key, Value = x.Value.ToString() }));

            var obj = dataService.GetObject(objectIdStr, type, bindingPath);
            PropertyGrid = viewService.GetPropertyGrid(obj, parameters, bindingPath);

        }
    }
}
