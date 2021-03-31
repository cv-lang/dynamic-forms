using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Tools;

namespace Cvl.DynamicForms.Services
{
    /// <summary>
    /// Serwis do pobierania danych - obiektów, kolekcji
    /// </summary>
    public class DataService
    {
        public object GetObject(long objectId)
        {
            var tp = new Test.TestPerson() { Firstname = "Jan", Surname = "Kowalski", Age = 35, IsEmployed = true, DwellingPlace = Test.Place.Krakow, Earnings = 3.85F  };
            tp.Address = new Test.Address() { City = "Kraków", Street = "Jana Pawła II" };
            tp.Invoices.Add(new Test.Invoice() { Number = "1/2021", Net = 100, Gross = 123 });
            tp.Invoices.Add(new Test.Invoice() { Number = "2/2021", Net = 100, Gross = 123 });
            tp.Invoices.Add(new Test.Invoice() { Number = "3/2021", Net = 100, Gross = 123 });

            if (objectId == 12)
            {
                var xml = Serializer.SerializeObject(tp);
                return new ObjectXmlWrapper(xml);
            }

            return tp;
        }
    }
}
