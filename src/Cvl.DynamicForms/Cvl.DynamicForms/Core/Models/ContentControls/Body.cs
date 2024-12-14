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
    public class Body : ContentControl
    {
        public Body(ControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Body;
        }
    }

    public class BodyParser : IContentControlParser
    {
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new Body(controlDescription);
        }

        public bool IsContentControl(string name)
        {
            return name.ToLower() == "body";
        }
    }
}