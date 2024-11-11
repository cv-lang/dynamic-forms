using Cvl.DynamicForms.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Importers.Excel
{
    public interface IFormSectionTypeParser
    {
        ControlType Parse(string elementType, int row);
    }

    public class FormSectionTypeParser : IFormSectionTypeParser
    {
        public ControlType Parse(string elementType, int row)
        {
            var validTypeName = new string[] { "sekcja","wiersz", "row","kolumna","column", "tabs", "tab", "tabela", "grid", "container", "kontener", "grid", "legend", "legenda"};

            if (validTypeName.Any(x => elementType == x) == false)
            {
                throw new Exception($"Błędna Sekcja: {elementType} w wierszu: {row}. Dostępne typy to {string.Join(",", validTypeName)}");
            }


            switch (elementType)
            {
                case "legend":
                case "legenda":
                    return ControlType.Legend;
                case "grid":
                    return ControlType.Grid;
                case "container":
                case "kontener":
                    return ControlType.Container;
                case "sekcja":
                    return ControlType.Section;
                case "row":
                case "wiersz":
                    return ControlType.Row;
                case "kolumna":
                case "column":
                    return ControlType.Column;
                case "tabs":
                    return ControlType.Tabs;
                case "tab":
                    return ControlType.Tab;
                case "tabela":
                    return ControlType.Table;
            }

            throw new Exception("Brak takiego typu");
        }
    }
}
