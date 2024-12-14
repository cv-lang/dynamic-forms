using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.ContentControls
{
    public class AutoComplete : ContentControl
    {
        public AutoComplete(ControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.AutoComplete;
        }
    }

    public class AutoCompleteParser : IContentControlParser
    {
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new AutoComplete(controlDescription);
        }

        public bool IsContentControl(string name)
        {
            return name.ToLower() == "autocomplete";
        }
    }
}