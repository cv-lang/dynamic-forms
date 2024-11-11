using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.DynamicForms.Importers.Excel.Tools;

namespace Cvl.DynamicForms.Importers.Excel
{
    public interface IWorksheetValidator
    {
        public void Validate(IWorksheet ws);
    }

    internal class WorksheetValidator : IWorksheetValidator
    {
        public void Validate(IWorksheet ws)
        {
            if (ws.GetCellText(1, 1)?.ToLower() != "df8-fd")
                throw new Exception("Poprawny szablon musi mieć w komórce A1 tekst: df8-fd");

            if (ws.GetCellText(1, 2) != "ver")
                throw new Exception("Poprawny szablon musi mieć w komórce B1 tekst: ver");

            var columns = new string[] {"1","2","3","4","5","6","Nazwa","Typ", "*","RO", "Opis",
                "Źródło danych","Ikona", "Znak wodny", "Wyrównanie",  "Walid","Walid. Wiadomość","Tooltip",
                "Wartość","Binding","Zaznaczony element","Akcja","Uwagi" };

            for (int i = 0; i < columns.Length; i++)
            {
                if (ws.GetCellText(3, i + 1) != columns[i])
                {
                    throw new Exception($"Poprawny szablon musi mieć w komórce {char.ConvertFromUtf32(65 + i)}3 tekst: '{columns[i]}', a ma '{ws.GetCellText(2, i + 1)}'. " +
                        $"Lista wszystkich kolumn od A3 do S3 [{string.Join(",", columns)}]");
                }

            }


        }
    }
}
