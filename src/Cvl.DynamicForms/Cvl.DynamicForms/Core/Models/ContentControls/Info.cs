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
    // Klasa Info reprezentująca kontrolkę wyświetlania informacji
    public class Info : ContentControl
    {
        public Info(ControlDescription controlDescription) : base(controlDescription)
        {
        }
    }

    // Parser dla kontrolki Info
    public class InfoParser : IContentControlParser
    {
        // Metoda tworząca instancję kontrolki Info
        public ContentControl Create(ControlDescription controlDescription)
        {
            return new Info(controlDescription);
        }

        // Metoda sprawdzająca, czy nazwa kontrolki odpowiada informacyjnej
        public bool IsContentControl(string name)
        {
            return new string[] { "info", "informacja" }.Contains(name.ToLower());
        }
    }
}
