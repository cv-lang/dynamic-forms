using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Test;
using Cvl.DynamicForms.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        private List<Logger> loggers = new List<Logger>();

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

                var number10 = number % 10;
                if (number10 == 9)
                {
                    tp.Address = null;
                    tp.Invoices = null;
                }

                int ilog = 0;
                var log = new Logger();
                log.Id = ilog++;
                log.Member = "Pozim 0";
                log.Message = $"Message 0 {log.Id}";
                loggers.Add(log);
                for (int i = 0; i < 20; i++)
                {
                    var log2 = new Logger();
                    log2.Id = ilog++;

                    loggers.Add(log2);
                    log2.ParentId = log.Id;
                    log.Subloggers.Add(log2);

                    log2.Member = "Pozim 1";
                    log2.Message = $"Message 1 {log2.Id}";

                    for (int i3 = 0; i3 < 10; i3++)
                    {
                        var log3 = new Logger();
                        log3.Id = ilog++;

                        loggers.Add(log3);
                        log3.ParentId = log2.Id;
                        log2.Subloggers.Add(log3);

                        log3.Member = "Pozim 2";
                        log3.Message = $"Message 2 {log3.Id}";
                    }
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

            IQueryable<object> collection = getCollection(typeFullname);

            var tp = collection.Skip(id).Take(1).FirstOrDefault();
            return tp;
        }
        

        public IQueryable<object> GetCollection(CollectionViewModelParameters parameters)
        {
            switch (parameters.CollectionTypeName)
            {
                case "TestPerson":
                    return people.Cast<object>().AsQueryable();
                case "Address":
                    return addresses.Cast<object>().AsQueryable();
                case "Invoice":
                    return invoices.Cast<object>().AsQueryable();
                case "Logger":
                    var lq = loggers;                    
                    lq = parameters.Id == null ? lq : lq.Where(x => x.Id == parameters.Id).ToList();
                    lq = parameters.ParentId == null ? lq : lq = lq.Where(x => x.ParentId == parameters.ParentId).ToList();
                    return lq.Cast<object>().AsQueryable();
            }

            return new List<object>().AsQueryable();
        }

        private IQueryable<object> getCollection(string collectionTypeName)
        {
            switch (collectionTypeName)
            {
                case "TestPerson":
                    return people.Cast<object>().AsQueryable();
                case "Address":
                    return addresses.Cast<object>().AsQueryable();
                case "Invoice":
                    return invoices.Cast<object>().AsQueryable();
                case "Logger":
                    return loggers.Cast<object>().AsQueryable();
            }
            return new List<object>().AsQueryable();
        }


        public string GetIdPropertyName(Type valueType)
        {
            return "Id";
        }
    }
}
