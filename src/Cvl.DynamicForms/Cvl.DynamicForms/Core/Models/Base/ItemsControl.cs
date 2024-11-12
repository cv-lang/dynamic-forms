using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Base
{
    /// <summary>
    /// Klasa bazowa dla wszsytkich kontrolek które są bardziej złożone i mają itemy danych, datagrid, listbox, treeview itp
    /// </summary>
    public class ItemsControl : HierarchicalControl
    {

        
        public override string ToString()
        {
            return $"{Name} - {Type}";
        }
    }


}
