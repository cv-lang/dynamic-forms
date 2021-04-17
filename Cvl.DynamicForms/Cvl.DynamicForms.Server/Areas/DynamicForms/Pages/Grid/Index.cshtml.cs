using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cvl.DynamicForms.Base;
using Cvl.DynamicForms.Model.ViewModel;
using Cvl.DynamicForms.Services;
using Cvl.DynamicForms.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.Grid
{
    public class IndexModel : PageModelBase
    {
        private readonly GridService viewService;
        public GridVM GridViewModel { get; set; }

        public string RefreshUrl { get; set; }
        public string AutoRefreshUrl { get; set; }
        public bool IsAutoRefresh { get; set; }

        public IndexModel(DataServiceBase dataService, ViewConfigurationService viewConfigurationService, ApplicationConfigurtion applicationUrlConfigurtion) : base(applicationUrlConfigurtion)
        {
            this.viewService = new GridService(dataService, viewConfigurationService);
        }
        public void OnGet()
        {
            SetBasePage();
            var query = Request.Query;
            var type = query["type"];
            var parentIdStr = query["parentId"];
            var parentType = query["parentType"];

            var autorefresh = query["autorefresh"];

            if (string.IsNullOrEmpty(autorefresh) == false)
            {
                IsAutoRefresh = true;
                Response.Headers.Add("Refresh", autorefresh);
            }
            RefreshUrl = $"{ApplicationUrl}{Request.Path}?type={type}&parentId={parentIdStr}&parentType={parentType}";
            AutoRefreshUrl = RefreshUrl + "&autorefresh=3";

            var param = new CollectionViewModelParameters();
            GridViewModel = viewService.GetGridViewModelForType(type, parentIdStr, parentType,  param);
            GridViewModel.MainObjectTypeFullname = type;
        }
    }
}
