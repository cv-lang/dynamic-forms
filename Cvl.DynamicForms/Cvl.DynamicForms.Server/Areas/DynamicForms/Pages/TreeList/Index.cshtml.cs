using System;
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
        public string Filter { get; set; }
        public bool WasButtonPressed { get; set; }
        public string RefreshUrl { get; set; }
        public string AutoRefreshUrl { get; set; }
        public bool IsAutoRefresh { get; set; }

        public TreeListViewModel TreeList { get; set; }

        public IndexModel(DataServiceBase dataService, ViewConfigurationService viewConfigurationService, ApplicationConfigurtion applicationConfigurtion, TreeListService treeListService) : base(applicationConfigurtion)
        {
            this.dataService = dataService;
            this.viewService = treeListService;
        }
        public void OnGet(string id, string type, string mainfilter)
        {
            SetBasePage();
            bool wasButtonPressed;
            var query = Request.Query;           
            var autorefresh = query["autorefresh"];

            if (string.IsNullOrEmpty(autorefresh) == false)
            {
                IsAutoRefresh = true;
                Response.Headers.Add("Refresh", autorefresh);
            }
            RefreshUrl = $"{ApplicationUrl}{Request.Path}?type={type}&id={id}";
            AutoRefreshUrl = RefreshUrl + "&autorefresh=3";

            var p = new CollectionViewModelParameters();
            TreeList = (TreeListViewModel)viewService.GetTreeList(id, type, p, mainfilter);
            TreeList.MainObjectTypeFullname = type;
            TreeList.MainObjectId = id;
        }
    }
}
