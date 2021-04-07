using System.Linq;
using Cvl.DynamicForms.Model.ViewModel;
using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.TreeList
{
    public class IndexModel : PageModel
    {
        private readonly DataService dataService;
        private readonly TreeListService viewService;

        public TreeListViewModel TreeList { get; set; }

        public IndexModel(DataService dataService, ViewConfigurationService viewConfigurationService)
        {
            this.dataService = dataService;
            this.viewService = new TreeListService(dataService, viewConfigurationService);
        }

        public void OnGet()
        {
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var objectId = int.Parse(objectIdStr);
            var type = query["type"];

            var parameters = new Parameters(query.Select(x => new Parameter() { Key = x.Key, Value = x.Value.ToString() }));

            var p = new TreeListParameters();
            p.ParentId = objectId;
            p.CollectionTypeName = type;


            TreeList = viewService.GetTreeList(p);

        }
    }
}
