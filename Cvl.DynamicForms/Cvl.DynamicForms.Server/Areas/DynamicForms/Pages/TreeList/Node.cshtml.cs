using System.Linq;
using Cvl.DynamicForms.Model.ViewModel;
using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.TreeList
{
    public class NodeModel : PageModel
    {
        private readonly DataService dataService;
        private readonly TreeListService viewService;

        public TreeListViewModel TreeList { get; set; }

        public NodeModel(DataService dataService, ViewConfigurationService viewConfigurationService)
        {
            this.dataService = dataService;
            this.viewService = new TreeListService(dataService, viewConfigurationService);
        }

        public void OnGet()
        {
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            //var objectId = int.Parse(objectIdStr);
            var type = query["type"];

            var parameters = new Parameters(query.Select(x => new Parameter() { Key = x.Key, Value = x.Value.ToString() }));

            var p = new TreeListParameters();
            p.ParentId = 1;// objectId;
            p.CollectionTypeName = "Logger";// type;


            TreeList = viewService.GetTreeList(p);

        }
    }
}
