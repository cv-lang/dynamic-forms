using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cvl.DynamicForms.Base;
using Cvl.DynamicForms.Services;
using Cvl.DynamicForms.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.PropertyGrid
{
    public class GetPropertyValueModel : PageModelBase
    {
        private readonly DataServiceBase dataService;

        public GetPropertyValueModel(DataServiceBase dataService, ApplicationConfigurtion applicationConfigurtion) : base(applicationConfigurtion)
        {
            this.dataService = dataService;
        }

        public string BindingPath { get; set; }
        public string Value { get; set; }

        public void OnGet()
        {
            SetBasePage();
            var query = Request.Query;
            var objectIdStr = query["id"].ToString();
            var type = query["type"];
            BindingPath = query["bindingPath"];

            var val = dataService.GetObject(objectIdStr, type, BindingPath);

            Value = val?.ToString() ?? "NULL";
        }
    }
}
