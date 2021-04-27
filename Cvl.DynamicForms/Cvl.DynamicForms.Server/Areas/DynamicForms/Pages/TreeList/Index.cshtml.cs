using System.Linq;
using Cvl.DynamicForms.Base;
using Cvl.DynamicForms.Model.ViewModel;
using Cvl.DynamicForms.Services;
using Cvl.DynamicForms.Services.Base;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.TreeList
{
    public class IndexModel : PageModelBase
    {
        private readonly DataServiceBase dataService;
        private readonly TreeListService viewService;

        public string RefreshUrl { get; set; }
        public string AutoRefreshUrl { get; set; }
        public bool IsAutoRefresh { get; set; }

        public TreeListViewModel TreeList { get; set; }

        public IndexModel(DataServiceBase dataService, ViewConfigurationService viewConfigurationService, ApplicationConfigurtion applicationConfigurtion) : base(applicationConfigurtion)
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
            var autorefresh = query["autorefresh"];

            if (string.IsNullOrEmpty(autorefresh) == false)
            {
                IsAutoRefresh = true;
                Response.Headers.Add("Refresh", autorefresh);
            }
            RefreshUrl = $"{ApplicationUrl}{Request.Path}?type={type}&id={objectIdStr}";
            AutoRefreshUrl = RefreshUrl + "&autorefresh=3";

            var p = new CollectionViewModelParameters();
            TreeList = (TreeListViewModel)viewService.GetTreeList(objectIdStr, type, p);
        }
    }
}
