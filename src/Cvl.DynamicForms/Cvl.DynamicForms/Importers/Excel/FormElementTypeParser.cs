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
        FormElementType Parse(string elementType, int row);
    }
    internal class FormElementTypeParser : IFormElementTypeParser
    {
        public FormElementType Parse(string elementType, int row)
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
                    return FormElementType.Legend;
                case "cb":
                case "checkbox":
                    return FormElementType.Bool;
                case "tekst":
                    return FormElementType.Text;
                case "data":
                    return FormElementType.Date;
                case "combo":
                    return FormElementType.Combo;
                case "przycisk":
                    return FormElementType.Button;
                case "img":
                    return FormElementType.Icon;
                case "info":
                    return FormElementType.Info;
                case "waluta":
                    return FormElementType.Currency;

            }

            return FormElementType.Auto;
        }
    }
}
