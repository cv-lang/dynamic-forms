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
    public class Currency : ContentControl
    {
        public Currency(ControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Currency;
        }
    }

    public class CurrencyParser : IContentControlParser
    {
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new Currency(controlDescription);
        }

        public bool IsContentControl(string name)
        {
            return new string[] { "currency", "waluta"}.Contains(name.ToLower());
        }
    }
}
