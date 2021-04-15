using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.Components.GridView
{
    public class GridViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Model.ViewModel.GridVM gridViewModel)
        {
            return View(gridViewModel);
        }
    }
}
