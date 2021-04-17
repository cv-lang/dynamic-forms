using System.Linq;
using Cvl.DynamicForms.Base;
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

        public TreeListViewModel TreeList { get; set; }

        public NodeModel(DataServiceBase dataService, ViewConfigurationService viewConfigurationService, ApplicationConfigurtion applicationConfigurtion) : base(applicationConfigurtion)
        {
            this.dataService = dataService;
            this.viewService = new TreeListService(dataService, viewConfigurationService);
        }

        public void OnGet()
        {
            SetBasePage();
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var type = query["type"];

            var p = new CollectionViewModelParameters();
            TreeList = viewService.GetTreeList(objectIdStr, type, p);
        }
    }
}
