using Cvl.DynamicForms.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.Components.GroupView
{
    public class GroupViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PropertyGroupViewModel groupViewModel)
        {
            return View(groupViewModel);
        }
    }
}
