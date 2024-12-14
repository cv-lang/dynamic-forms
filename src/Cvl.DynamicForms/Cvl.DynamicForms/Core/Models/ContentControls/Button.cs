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
    // Klasa Button reprezentująca kontrolkę przycisku
    public class Button : ContentControl
    {
        public Button(ControlDescription controlDescription) : base(controlDescription)
        {
            Type = ControlType.Button;
        }
    }

    // Parser dla kontrolki Button
    public class ButtonParser : IContentControlParser
    {
        // Metoda tworząca instancję przycisku
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new Button(controlDescription);
        }

        // Metoda sprawdzająca, czy nazwa kontrolki odpowiada przyciskowi
        public bool IsContentControl(string name)
        {
            return new string[] { "button", "przycisk" }.Contains(name.ToLower());
        }
    }
}
