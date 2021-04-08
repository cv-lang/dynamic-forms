using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.Components.TreeListView
{
    public class TreeNodeViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TreeListViewModel treeListViewModel)
        {
            return View(treeListViewModel);
        }
    }
}
