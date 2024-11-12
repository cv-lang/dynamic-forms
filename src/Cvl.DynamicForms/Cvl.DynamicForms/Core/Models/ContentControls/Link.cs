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
    public class Link : ContentControl
    {
        public Link(ControlDescription controlDescription) : base(controlDescription)
        {
        }
    }

    public class LinkParser : IContentControlParser
    {
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new Link(controlDescription);
        }

        public bool IsContentControl(string name)
        {
            return name.ToLower() == "link";
        }
    }
}