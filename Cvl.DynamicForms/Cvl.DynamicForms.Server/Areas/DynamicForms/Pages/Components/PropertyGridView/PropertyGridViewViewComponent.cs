using Cvl.DynamicForms.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.Components.PropertyGridView
{
    public class PropertyGridViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PropertyGridViewModel propertyGridViewModel)
        {
            return View(propertyGridViewModel);
        }
    }
}
