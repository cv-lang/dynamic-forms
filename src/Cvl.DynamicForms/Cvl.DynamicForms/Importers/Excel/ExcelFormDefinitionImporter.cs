using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Xml.Linq;
using Cvl.DynamicForms.Core.Models;
using Cvl.DynamicForms.Core.Importer;

namespace Cvl.DynamicForms.Importers.Excel
{
    internal class ExcelFormDefinitionImporter(
        IFormElementTypeParser formElementTypeParser,
        IFormSectionTypeParser formSectionTypeParser,
        IExcelReader excelReader
        ) : IFormDefinitionImporter
    {
        public FormDefinition LoadFromFile(string filename, int index)
        {
            WorksheetValidator worksheetValidator = new();

            var ws = excelReader.GetWorksheet(filename, index);
            var name = $"{Path.GetFileName(filename)} - {ws.Name}";

            worksheetValidator.Validate(ws);

            var viewDescription = $"{name}, {filename}, Numer arkusza:{index}";

            var root = ParseExcelView(ws);


            var formDefinition = new FormDefinition()
            {
                Name = name,
                RootSection = root,
            };

            return formDefinition;
        }


        private FormSection ParseExcelView(IWorksheet ws)
        {
            var elementTypeParser = new FormElementTypeParser();
            int level = 1;
            var root = new FormSection() { Name = "root", Id = "1" };
            Stack<FormSection> controlStack = new();
            controlStack.Push(root);
            for (int row = 4; row < 2000; row++)
            {
                var controlName = ws.GetCellText(row, level);
                var elementName = ws.GetCellText(row, ExcelColumnIndex.Name) ?? "";
                var elementValue = ws.GetCellText(row, ExcelColumnIndex.Value);
                var placeholder = ws.GetCellText(row, ExcelColumnIndex.Placeholder) ?? "";
                var isRequiredString = ws.GetCellText(row, ExcelColumnIndex.IsRequired);
                var isRequired = isRequiredString == "1";
                var description = ws.GetCellText(row, ExcelColumnIndex.Description) ?? "";
                var datasource = ws.GetCellText(row, ExcelColumnIndex.Datasource) ?? "";
                var binding = ws.GetCellText(row, ExcelColumnIndex.Binding) ?? elementName.Replace(" ", "_");
                var stringType = ws.GetCellText(row, ExcelColumnIndex.Type)?.Trim() ?? "tekst";
                var type = formElementTypeParser.Parse(stringType, row);

                var action = ws.GetCellText(row, ExcelColumnIndex.Action)?.Trim()?.ToLower();

                var currentControl = controlStack.Peek();

                var czyWszystkiePuste = new int[] { 1, 2, 3, 4, 5 }.All(x => string.IsNullOrEmpty(ws.GetCellText(row, x)));

                if (czyWszystkiePuste && string.IsNullOrEmpty(elementName) == false)
                {
                    //mamy element
                    var element = new FormElement()
                    {
                        Id = row.ToString(),
                        Name = elementName,
                        Type = type,
                        Value = elementValue,
                        Placeholder = placeholder,
                        IsRequired = isRequired,
                        Description = $"{description} - path:{binding}",
                        DataSource = datasource,
                        Action = action
                    };

                    currentControl.Elements.Add(element);
                    continue;
                }

                if (czyWszystkiePuste)
                {
                    continue;
                }
                
                if (controlName == "sekcja" || controlName == "kolumna" 
                    || controlName == "wiersz" || controlName == "tabs"
                    || controlName == "tab" || controlName == "tabela")
                {
                    var ctrl = new FormSection()
                    {
                        Id = row.ToString(),
                        Name = elementName ?? "",
                        SectionType = formSectionTypeParser.Parse(controlName, row)
                    };
                    currentControl.Children.Add(ctrl);
                    controlStack.Push(ctrl);
                    level++;
                    continue;
                } 



                //możemy mieć zmniejszenie poziomu
                var nowyPoziom = new int[] { 1, 2, 3, 4, 5 }.First(x => string.IsNullOrEmpty(ws.GetCellText(row, x)) == false);
                var dl = level - nowyPoziom;
                for (int i = 0; i < dl; i++)
                {
                    controlStack.Pop();
                }
                row--;
                level = nowyPoziom;
            }

            return root;
        }

    }

    public class ExcelColumnIndex
    {
        public const int Name = 6;
        public const int Type = 7;
        public const int Value = 8;
        public const int Binding = 9;
        public const int Placeholder = 10;
        public const int IsRequired = 11;
        public const int Description = 13;
        public const int Datasource = 16;
        public const int Action = 17;
        public const int IsReadOnly = 19;
    }
}
