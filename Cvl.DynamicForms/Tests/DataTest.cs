using NUnit.Framework;
using Cvl.DynamicForms.Services;
using Cvl.DynamicForms.Test;
using System;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DataEditTest1()
        {
            var service = new DataServiceBase();
            var testobj = new Logger()
            {
                Id = 0,
                Date = DateTime.Now,
                Message = "er",
                SourceCodeLine = 10
            };
            testobj.Subloggers.Add(new Logger()
            {
                Id = 2,
                Date = DateTime.Now,
                Message = "errrrrr",
                SourceCodeLine = 1000
            });
            var editservice = new DataWriter();
            editservice.SetCustomValue(testobj, "Message", "osiem");
            editservice.SetCustomValue(testobj, "Date", "03/01/2009 05:42:00");
            editservice.SetCustomValue(testobj, "Subloggers[0].Message", 3);
            Assert.AreEqual("osiem", testobj.Message);
            Assert.AreEqual("3", testobj.Subloggers.First().Message);
        }
    }
}