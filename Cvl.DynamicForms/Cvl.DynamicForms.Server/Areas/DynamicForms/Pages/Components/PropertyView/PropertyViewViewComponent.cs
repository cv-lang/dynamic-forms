using Cvl.DynamicForms.Model;
using Microsoft.AspNetCore.Mvc;

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
