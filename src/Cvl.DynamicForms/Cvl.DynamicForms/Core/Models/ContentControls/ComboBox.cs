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
    // Klasa ComboBox reprezentująca kontrolkę listy rozwijanej
    public class ComboBox : ContentControl
    {
        public ComboBox(ControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.ComboBox;
        }
    }

    // Parser dla kontrolki ComboBox
    public class ComboBoxParser : IContentControlParser
    {
        // Metoda tworząca instancję listy rozwijanej
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new ComboBox(controlDescription);
        }

        // Metoda sprawdzająca, czy nazwa kontrolki odpowiada liście rozwijanej
        public bool IsContentControl(string name)
        {
            return new string[] { "combobox", "cb", "dropdown" }.Contains(name.ToLower());
        }
    }
}
