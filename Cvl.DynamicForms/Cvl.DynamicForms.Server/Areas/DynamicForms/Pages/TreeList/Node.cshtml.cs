using System.Linq;
using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.DynamicForms.Areas.DynamicForms.Pages.TreeList
{
    public class NodeModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
