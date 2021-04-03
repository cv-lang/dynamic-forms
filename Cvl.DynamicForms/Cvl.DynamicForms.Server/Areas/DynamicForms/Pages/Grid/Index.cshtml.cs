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
        private readonly DataService dataService;
        private readonly GridViewModelFactory viewService;

        public GridViewModel GridViewModel { get; set; }

        public IndexModel(DataService dataService, GridViewModelFactory viewService)
        {
            this.dataService = dataService;
            this.viewService = viewService;
        }
        public void OnGet()
        {
            var query = Request.Query;
            var type = query["type"];

            var parameters = new Parameters(query.Select(x => new Parameter() { Key = x.Key, Value = x.Value.ToString() }));
            var obj = dataService.GetCollection(type);
            GridViewModel = viewService.GetGridViewModel(obj, new GridViewModelParameters());

        }
    }
}
