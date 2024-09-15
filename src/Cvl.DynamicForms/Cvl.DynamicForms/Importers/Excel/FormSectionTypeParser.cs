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
        FormSectionType Parse(string elementType, int row);
    }

    public class FormSectionTypeParser : IFormSectionTypeParser
    {
        public FormSectionType Parse(string elementType, int row)
        {
            var validTypeName = new string[] { "sekcja","wiersz", "row","kolumna","column", "tabs", "tab", "tabela"};

            if (validTypeName.Any(x => elementType == x) == false)
            {
                throw new Exception($"Błędna Sekcja: {elementType} w wierszu: {row}. Dostępne typy to {string.Join(",", validTypeName)}");
            }


            switch (elementType)
            {
                case "sekcja":
                    return FormSectionType.Section;
                case "legend":
                    return FormSectionType.Legend;
                case "row":
                case "wiersz":
                    return FormSectionType.Row;
                case "kolumna":
                case "column":
                    return FormSectionType.Column;
                case "tabs":
                    return FormSectionType.Tabs;
                case "tab":
                    return FormSectionType.Tab;
                case "tabela":
                    return FormSectionType.Table;
            }

            throw new Exception("Brak takiego typu");
        }
    }
}
