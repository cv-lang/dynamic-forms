using Cvl.DynamicForms.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Pages.Components.PropertyView
{
    public class PropertyViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PropertyViewModel propertyViewModel)
        {
            return View(propertyViewModel);
        }
    }
}
