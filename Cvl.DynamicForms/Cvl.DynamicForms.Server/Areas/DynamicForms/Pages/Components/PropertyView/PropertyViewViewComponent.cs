using Cvl.DynamicForms.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cvl.DynamicForms.Pages.Components.PropertyView
{
    public class PropertyViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PropertyBaseVM propertyViewModel)
        {
            return View(propertyViewModel);
        }
    }
}
