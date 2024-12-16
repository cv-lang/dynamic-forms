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
    public class TextBox : ContentControl
    {
        public TextBox(ControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.TextBox;
        }
    }

    public class TextBoxParser : IContentControlParser
    {
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new TextBox(controlDescription);
        }

        public bool IsContentControl(string name)
        {
            return  new string[]{ "textbox", "tekst", "auto"}.Contains(name.ToLower());
        }
    }
}