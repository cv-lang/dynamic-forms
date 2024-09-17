using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models
{
    public class ItemsControl : Control
    {        
        
        public List<Control> Children { get; set; } = new List<Control>();
        public override string ToString()
        {
            return $"{Name} - {Type}";
        }
    }

    
}
