using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.ContentControls;
using Cvl.DynamicForms.Core.Models.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Base
{
    /// <summary>
    /// Klasa bazowa dla wszystkich kontrolek które są contenerami, panele, stacki row itp
    /// </summary>
    
    public class ContainerControl : HierarchicalControl
    {
        public ContainerControl()
        {
        }
        public ContainerControl(HierarchicalControlDescription controlDescription)
        {
        }
    }
}
