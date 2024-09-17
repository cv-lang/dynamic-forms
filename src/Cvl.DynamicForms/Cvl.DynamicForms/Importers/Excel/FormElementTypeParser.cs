using Cvl.DynamicForms.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cvl.DynamicForms.Importers.Excel
{
    public interface IFormElementTypeParser
    {
        ContentControlType Parse(string elementType, int row);
    }
    internal class FormElementTypeParser : IFormElementTypeParser
    {
        public ContentControlType Parse(string elementType, int row)
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
                    return ContentControlType.Legend;
                case "cb":
                case "checkbox":
                    return ContentControlType.Checkbox;
                case "tekst":
                    return ContentControlType.Text;
                case "data":
                    return ContentControlType.Date;
                case "combo":
                    return ContentControlType.Combo;
                case "przycisk":
                    return ContentControlType.Button;
                case "img":
                    return ContentControlType.Icon;
                case "info":
                    return ContentControlType.Info;
                case "waluta":
                    return ContentControlType.Currency;

            }

            return ContentControlType.Auto;
        }
    }
}
