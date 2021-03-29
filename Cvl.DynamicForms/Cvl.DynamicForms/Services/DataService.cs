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
            return new Test.TestPerson() { Firstname = "Jan", Surname = "Kowalski" };
        }
    }
}
