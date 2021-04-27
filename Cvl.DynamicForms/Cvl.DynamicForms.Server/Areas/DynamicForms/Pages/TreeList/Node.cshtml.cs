using System.Linq;
using Cvl.DynamicForms.Base;
using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Model.ViewModel;
using Cvl.DynamicForms.Services;
using Cvl.DynamicForms.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.TreeList
{
    public class NodeModel : PageModelBase
    {
        private readonly DataServiceBase dataService;
        private readonly TreeListService viewService;
        public string id;

        public RegionVM Region { get; set; }

        public NodeModel(DataServiceBase dataService, ViewConfigurationService viewConfigurationService, ApplicationConfigurtion applicationConfigurtion, TreeListService treeListService) : base(applicationConfigurtion)
        {
            this.dataService = dataService;
            this.viewService = treeListService;
        }

        public void OnGet()
        {
            SetBasePage();
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var type = query["type"];
            id = objectIdStr;
            var p = new CollectionViewModelParameters();
            Region = viewService.GetTreeList(objectIdStr, type, p);
        }
    }
}
