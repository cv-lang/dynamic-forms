using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Test;
using Cvl.DynamicForms.Tools;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cvl.DynamicForms.Services
{
    /// <summary>
    /// Serwis do pobierania danych - obiektów, kolekcji
    /// </summary>
    public class DataService
    {
        private List<TestPerson> people = new List<TestPerson>();
        private List<Address> addresses = new List<Address>();
        private List<Invoice> invoices = new List<Invoice>();

        private void generate()
        {
            for (int number = 0; number < 200; number++)
            {
                var tp = new TestPerson() { Id = number, Age = 18 + number % 30, IsEmployed = true, DwellingPlace = Test.Place.Krakow, Earnings = 3.85F };
                people.Add(tp);

                var number3 = number % 3;
                switch (number3)
                {
                    case 0:
                        tp.Firstname = "Jan";
                        break;
                    case 1:
                        tp.Firstname = "Roman";
                        break;
                    case 2:
                        tp.Firstname = "Adam";
                        break;
                }

                var number30 = (number / 3) % 3;
                switch (number30)
                {
                    case 0:
                        tp.Surname = "Kowalski";
                        break;
                    case 1:
                        tp.Surname = "Nowak";
                        break;
                    case 2:
                        tp.Surname = "Kowal";
                        break;
                }

                var address = new Address() { Id= number, City = "Kraków", Street = $"Jana Nowakowskiego {number}", Postcode = "11-222" };
                addresses.Add(address);
                tp.Address = address;

                var invoiceCount = Math.Min(Math.Max(3, number), 100);
                for (int i = 0; i < invoiceCount; i++)
                {
                    var invoice = new Invoice() {Id= number*100+i,  Number = $"{i}/2021", Net = 100 * i, Gross = 123 * 1 };
                    invoices.Add(invoice);
                    tp.Invoices.Add(invoice);
                }
            }
        }

        public DataService()
        {
            generate();
        }

        public object GetObject(string objectId, string typeFullname)
        {
            var id = int.Parse(objectId);
            var collection = GetCollection(typeFullname);


            var tp = people[id];
            return tp;
        }
                

        public ICollection GetCollection(string typeFullname)
        {
            switch(typeFullname)
            {
                case "TestPerson":
                    return people;
                case "Address":
                    return addresses;
                case "Invoice":
                    return invoices;
            }

            return new List<TestPerson>();
        }
    }
}
