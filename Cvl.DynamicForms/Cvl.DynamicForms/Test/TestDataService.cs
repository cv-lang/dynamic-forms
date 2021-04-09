using Cvl.DynamicForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.DynamicForms.Test
{
    public class TestDataService : DataServiceBase
    {
        private List<TestPerson> people = new List<TestPerson>();
        private List<Address> addresses = new List<Address>();
        private List<Invoice> invoices = new List<Invoice>();
        private List<Logger> loggers = new List<Logger>();

        private void generate()
        {
            int ilog = 1;
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



                var address = new Address() { Id = number, City = "Kraków", Street = $"Jana Nowakowskiego {number}", Postcode = "11-222" };
                addresses.Add(address);
                tp.Address = address;

                var invoiceCount = Math.Min(Math.Max(3, number), 100);
                for (int i = 0; i < invoiceCount; i++)
                {
                    var invoice = new Invoice() { Id = number * 100 + i, Number = $"{i}/2021", Net = 100 * i, Gross = 123 * 1 };
                    invoices.Add(invoice);
                    tp.Invoices.Add(invoice);
                }

                var number10 = number % 10;
                if (number10 == 9)
                {
                    tp.Address = null;
                    tp.Invoices = null;
                }


                var log = new Logger();
                log.Id = ilog++;
                log.Member = "Poziom 0";
                log.Message = $"Message 0 {log.Id}";
                loggers.Add(log);
                for (int i = 0; i < 20; i++)
                {
                    var log2 = new Logger();
                    log2.Id = ilog++;

                    loggers.Add(log2);
                    log2.ParentId = log.Id;
                    log.Subloggers.Add(log2);

                    log2.Member = "Poziom 1";
                    log2.Message = $"Message 1 {log2.Id}";

                    for (int i3 = 0; i3 < 10; i3++)
                    {
                        var log3 = new Logger();
                        log3.Id = ilog++;

                        loggers.Add(log3);
                        log3.ParentId = log2.Id;
                        log2.Subloggers.Add(log3);

                        log3.Member = "Poziom 2";
                        log3.Message = $"Message 2 {log3.Id}";
                    }
                }
            }
        }



        public TestDataService()
        {
            generate();
        }

        /// <summary>
        /// Zwraca pojedyńczy obiekt
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="typeFullname"></param>
        /// <returns></returns>
        public override object GetObject(string objectId, string typeFullname)
        {
            var id = int.Parse(objectId);

            switch (typeFullname)
            {
                case "Cvl.DynamicForms.Test.TestPerson":
                    return people.FirstOrDefault(x => x.Id == id);
                case "Cvl.DynamicForms.Test.Address":
                    return addresses.FirstOrDefault(x => x.Id == id);
                case "Cvl.DynamicForms.Test.Invoice":
                    return invoices.FirstOrDefault(x => x.Id == id);
                case "Cvl.DynamicForms.Test.Logger":
                    return loggers.FirstOrDefault(x => x.Id == id);
            }
            return null;
        }

        /// <summary>
        /// Zwraca dzieci obiektu - dla obiektu hierarchicznego
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override IQueryable<object> GetChildrenCollection(string objectId, string typeFullname, CollectionViewModelParameters parameters)
        {
            int? id = null;
            if (!string.IsNullOrEmpty(objectId) && objectId != "null")
            {
                id = int.Parse(objectId);
            }

            switch (typeFullname)
            {
                case "Cvl.DynamicForms.Test.Logger":
                    return loggers.Where(x => x.ParentId == id).Cast<object>().AsQueryable();
            }

            return null;
        }

        /// <summary>
        /// Zwraca kolekcję obiektów
        /// </summary>
        /// <param name="collectionTypeName"></param>
        /// <param name="objectId"></param>
        /// <param name="objectType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override IQueryable<object> GetCollection(string collectionTypeName, string objectId, string objectType, CollectionViewModelParameters parameters)
        {
            int? id = null;
            if (!string.IsNullOrEmpty(objectId) && objectId != "null")
            {
                id = int.Parse(objectId);
            }

            string objectTypeStr = null;
            if (!string.IsNullOrEmpty(objectType) && objectType != "null")
            {
                objectTypeStr = objectType;
            }

            switch (collectionTypeName)
            {
                case "Cvl.DynamicForms.Test.TestPerson":
                    return people.Cast<object>().AsQueryable();
                case "Cvl.DynamicForms.Test.Address":
                    return addresses.Cast<object>().AsQueryable();
                case "Cvl.DynamicForms.Test.Invoice":
                    return invoices.Cast<object>().AsQueryable();
                case "Cvl.DynamicForms.Test.Logger":
                    if (id == null)
                    {
                        return loggers.Cast<object>().AsQueryable();
                    }
                    else
                    {
                        return loggers.Where(x => x.ParentId == id).Cast<object>().AsQueryable();
                    }
            }
            return new List<object>().AsQueryable();
        }

        /// <summary>
        /// Zwraca nazwę propercji Id'ka
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public override string GetIdPropertyName(Type valueType)
        {
            return "Id";
        }
    }
}
