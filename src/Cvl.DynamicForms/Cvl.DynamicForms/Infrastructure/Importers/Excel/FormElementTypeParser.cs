using Cvl.DynamicForms.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cvl.DynamicForms.Infrastructure.Importers.Excel
{
    public interface IFormElementTypeParser
    {
        ControlType Parse(string elementType, int row);
    }
    internal class FormElementTypeParser : IFormElementTypeParser
    {
        public ControlType Parse(string elementType, int row)
        {
            var validTypeName = new string[] { "", "legend",
                "checkbox","cb", "tekst", "data", "combo", "przycisk", "img", "info", "waluta" };

            if (validTypeName.Any(x => elementType == x) == false)
            {
                throw new Exception($"Błędny typ: {elementType} w wierszu: {row}. Dostępne typy to {string.Join(",", validTypeName)}");
            }


            switch (elementType)
            {
                case "legend":
                    return ControlType.Legend;
                case "cb":
                case "checkbox":
                    return ControlType.Checkbox;
                case "tekst":
                    return ControlType.Text;
                case "data":
                    return ControlType.Date;
                case "combo":
                    return ControlType.Combo;
                case "przycisk":
                    return ControlType.Button;
                case "img":
                    return ControlType.Icon;
                case "info":
                    return ControlType.Info;
                case "waluta":
                    return ControlType.Currency;

            }

            return ControlType.Auto;
        }
    }
}
