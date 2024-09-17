using Cvl.DynamicForms.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Importers.Excel
{
    public interface IFormSectionTypeParser
    {
        ItemsControlType Parse(string elementType, int row);
    }

    public class FormSectionTypeParser : IFormSectionTypeParser
    {
        public ItemsControlType Parse(string elementType, int row)
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
                    return ItemsControlType.Legend;
                case "grid":
                    return ItemsControlType.Grid;
                case "container":
                case "kontener":
                    return ItemsControlType.Container;
                case "sekcja":
                    return ItemsControlType.Section;
                case "row":
                case "wiersz":
                    return ItemsControlType.Row;
                case "kolumna":
                case "column":
                    return ItemsControlType.Column;
                case "tabs":
                    return ItemsControlType.Tabs;
                case "tab":
                    return ItemsControlType.Tab;
                case "tabela":
                    return ItemsControlType.Table;
            }

            throw new Exception("Brak takiego typu");
        }
    }
}
