﻿using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
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
using Cvl.DynamicForms.Importers.Excel.Tools;
using Cvl.DynamicForms.Importers.Excel.Helpers;
using Cvl.DynamicForms.Core.Models.Base;
using Cvl.DynamicForms.Core.Parsers;
using Cvl.DynamicForms.Core.Models.Layouts;

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


        private HierarchicalControl ParseExcelView(IWorksheet ws)
        {
            var excelRowReader = new ExcelRowReader();
            var elementTypeParser = new FormElementTypeParser();
            int level = 1;
            var root = new Stack() { Name = "root", Id = "1" };
            Stack<HierarchicalControl> controlStack = new();
            controlStack.Push(root);
            for (int row = 4; row < 2000; row++)
            {
                var currentControl = controlStack.Peek();

                //sprawdzam rodzaj kontrolki
                var czyWszystkiePuste = new int[] { 1, 2, 3, 4, 5, 6 }.All(x => string.IsNullOrEmpty(ws.GetCellText(row, x)));

                if (czyWszystkiePuste && string.IsNullOrEmpty(ws.GetCellText(row, ExcelColumnIndex.Name)) == false)
                {
                    //mamy element
                    var controlDescription = excelRowReader.ReadControlRow(ws, row, level);
                    var controlsParser = new ContentControlsParser();

                    if (!controlsParser.IsContentControl(controlDescription))
                    {
                        throw new Exception("Element nie jest poprawną kontrolką");
                    }
                    
                    var ctrl = controlsParser.Create(controlDescription);
                    currentControl.Children.Add(ctrl);
                    continue;
                }

                if (czyWszystkiePuste)
                {
                    //mamy puste wiersze
                    continue;
                }

                var hierarchicalControlDescription = excelRowReader.ReadHierarchicalControlRow(ws, row, level);
                var hierarchicalParser = new HierarchicalControlsParser();

                if(hierarchicalParser.IsHierarhicalControl(hierarchicalControlDescription))
                {
                    var ctrl = hierarchicalParser.Create(hierarchicalControlDescription);

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
}
