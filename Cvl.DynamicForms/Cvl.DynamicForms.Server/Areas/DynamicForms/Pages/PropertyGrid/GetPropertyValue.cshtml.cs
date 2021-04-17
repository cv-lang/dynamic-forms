using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.PropertyGrid
{
    public class GetPropertyValueModel : PageModel
    {
        private readonly DataServiceBase dataService;

        public GetPropertyValueModel(DataServiceBase dataService)
        {
            this.dataService = dataService;
        }

        public string BindingPath { get; set; }
        public string Value { get; set; }

        public void OnGet()
        {
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var type = query["type"];
            BindingPath = query["bindingPath"];

            var val = dataService.GetObject(objectIdStr, type, BindingPath);

            Value = val?.ToString() ?? "NULL";
        }
    }
}
