using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Tools;
using System;
using System.Collections.Generic;
using System.Text;

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

            if(objectId == 12)
            {
                var xml = Serializer.SerializeObject(tp);
                return new ObjectXmlWrapper(xml);
            }

            return tp;
        }
    }
}
