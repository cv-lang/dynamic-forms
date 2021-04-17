using Cvl.DynamicForms.Services.Base;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Base
{
    public class PageModelBase : PageModel
    {
        private readonly ApplicationConfigurtion applicationUrlConfigurtion;
        public string ApplicationUrl { get; set; }

        public PageModelBase(ApplicationConfigurtion applicationUrlConfigurtion):base()
        {
            this.applicationUrlConfigurtion = applicationUrlConfigurtion;
            ApplicationUrl = applicationUrlConfigurtion.ApplicationUrl;            
        }

        public void SetBasePage()
        {
            ViewData["ApplicationUrl"] = ApplicationUrl;
        }
    }
}
