using System.Linq;
using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.PropertyGrid
{
    public class IndexModel : PageModel
    {
        private readonly DataServiceBase dataService;
        private readonly PropretyGridService viewService;

        public string RefreshUrl { get; set; }
        public string AutoRefreshUrl { get; set; }
        public bool IsAutoRefresh { get; set; }

        public Model.PropertyGridVM PropertyGrid { get; set; }

        public IndexModel(DataServiceBase dataService, ViewConfigurationService viewConfigurationService, PropretyGridService propretyGridService)
        {
            this.dataService = dataService;
            this.viewService = propretyGridService;
        }

        public void OnGet()
        {
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var type = query["type"];
            var autorefresh = query["autorefresh"];

            if(string.IsNullOrEmpty(autorefresh) == false)
            {
                IsAutoRefresh = true;
                Response.Headers.Add("Refresh", autorefresh);
            }
            RefreshUrl = $"{Request.Path}?id={objectIdStr}&type={type}";
            AutoRefreshUrl = RefreshUrl + "&autorefresh=3";

            PropertyGrid = viewService.GetPropertyGrid(objectIdStr, type,  "");     
        }
    }
}
