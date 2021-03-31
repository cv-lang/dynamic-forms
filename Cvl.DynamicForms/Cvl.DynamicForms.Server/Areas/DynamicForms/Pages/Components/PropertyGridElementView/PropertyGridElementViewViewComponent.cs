using Cvl.DynamicForms.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.Components.PropertyGridElementView
{
    public class PropertyGridElementViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PropertyGridElementViewModel propertyGridElementViewModel)
        {
            return View(propertyGridElementViewModel);
        }
    }
}
