using Cvl.DynamicForms.Core.ControlDescriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Base
{
    /// <summary>
    /// Klasa bazowa dla hierarchicznych kontrolke
    /// Hierearhiczne kontrolki to te które deklaruje się w kolumnach A-F - które mogą tworzyć drzewo kontrolek
    /// </summary>
    public abstract class HierarchicalControl : Control
    {
        
        public List<Control> Children { get; set; } = new List<Control>();
    }
}
