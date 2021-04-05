using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cvl.DynamicForms.Model.ViewModel;
using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.Grid
{
    public class IndexModel : PageModel
    {
        private readonly GridService viewService;

        public GridViewModel GridViewModel { get; set; }

        public IndexModel(DataService dataService, ViewConfigurationService viewConfigurationService)
        {
            this.viewService = new GridService(dataService, viewConfigurationService);
        }
        public void OnGet()
        {
            var query = Request.Query;
            var type = query["type"];
            var parentIdStr = query["parentId"];
            var parentType = query["parentType"];

            var parameters = new Parameters(query.Select(x => new Parameter() { Key = x.Key, Value = x.Value.ToString() }));
            var param = new CollectionViewModelParameters();
            param.CollectionTypeName = type.ToString();
            int parentId;
            int.TryParse(parentIdStr.ToString(), out parentId);
            param.ParentTypeName = parentType.ToString();
                

            GridViewModel = viewService.GetGridViewModel(param);

        }
    }
}
