using System.Linq;
using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.PropertyGrid
{
    public class IndexModel : PageModel
    {
        private readonly DataService dataService;
        private readonly PropretyGridFaktory viewService;
       
        public Model.PropertyGridViewModel PropertyGrid { get; set; }

        public IndexModel(DataService dataService, PropretyGridFaktory viewService)
        {
            this.dataService = dataService;
            this.viewService = viewService;
        }

        public void OnGet()
        {
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var objectId = long.Parse(objectIdStr);
            var type = query["type"];

            var parameters = new Parameters(query.Select(x => new Parameter(){ Key = x.Key, Value = x.Value.ToString() }));

            var obj = dataService.GetObject(objectIdStr, type);
            PropertyGrid = viewService.GetPropertyGrid(obj, parameters);                       
            
        }
    }
}
