using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;

namespace Cvl.DynamicForms.Core.Models.ContentControls
{
    public class Image : ContentControl
    {
        public Image(ControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Image;
        }
    }

    public class ImageParser : IContentControlParser
    {      
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new Image(controlDescription);
        }

        public bool IsContentControl(string name)
        {
            return name.ToLower() == "img";
        }
    }
}
