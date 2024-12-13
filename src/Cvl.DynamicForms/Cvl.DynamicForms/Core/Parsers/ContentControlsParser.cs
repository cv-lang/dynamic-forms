using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Models.ContentControls;
using Cvl.DynamicForms.Core.Models.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Parsers
{
    public interface IContentControlParser
    {
        bool IsContentControl(string name);
        ContentControl Create(ControlDescription controlDescription);
    }

    internal class ContentControlsParser
    {
        private static IContentControlParser[] parsers = { new ImageParser(), new LinkParser(), new BodyParser(),
        new DatePickerParser(), new TextBoxParser(), new AutoCompleteParser(), new ButtonParser(), new ComboBoxParser(),
        new CurrencyParser(), new InfoParser()};

        internal bool IsContentControl(ControlDescription controlDescription)
        {
            return parsers.Any(x => x.IsContentControl(controlDescription.ElementType));
        }

        internal ContentControl Create(ControlDescription controlDescription)
        {
            return parsers.First(x => x.IsContentControl(controlDescription.ElementType))
                .Create(controlDescription);
        }
    }
}
