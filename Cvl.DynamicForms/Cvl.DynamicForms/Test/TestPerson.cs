using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cvl.DynamicForms.Test
{
    [Serializable]
    [XmlRoot("Properties")]
    public class TestPerson
    {
        public int Id { get; set; }

        [Description("Imię")]
        [XmlElement]
        public string Firstname { get; set; }

        [Display(Description ="Nazwisko", GroupName = "Podstawowa", Order =1)]
        [XmlElement]
        public string Surname { get; set; }

        [Display(Description = "Wiek", GroupName = "Podstawowa", Order = 2)]
        [XmlElement]
        public int Age { get; set; }

        [Display(Description = "Zatrudnienie", GroupName = "Podstawowa", Order = 3)]
        [XmlElement]
        public bool IsEmployed { get; set; }

        [Display(Description = "Miejsce zamieszkania", GroupName = "Podstawowa", Order = 4)]
        [XmlElement]
        public Place DwellingPlace { get; set; }

        [Display(Description = "Zarobki(w tys. zł)", GroupName = "Podstawowa", Order = 5)]
        [XmlElement]
        public float Earnings { get; set; }

        public Address Address { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();


        public string BigStringXml { get; set; }
    }


    public class Invoice
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Net { get; set; }
        public decimal Gross { get; set; }

        public string BigStringTest { get; set; } = "sd asdf asdfasdfasdfas dfasdfasdfasdfkjakjhk jdhfka hkd jhaksjdh k" +
            "sdfa sdf asdf asdfasdfwerwerwerwefwwelkjlkjldkjflaksjdfasdfs sdf sdf sdf sdfs dfsd fsd fsd fsd fsd fdsf dsf" +
            "sdfa sdf asdf asdfasdfwerwerwerwefwwelkjlkjldkjflaksjdfasdfs sdf sdf sdf sdfs dfsd fsd fsd fsd fsd fdsf dsf" +
            "sdfa sdf asdf asdfasdfwerwerwerwefwwelkjlkjldkjflaksjdfasdfs sdf sdf sdf sdfs dfsd fsd fsd fsd fsd fdsf dsf" +
            "sdfa sdf asdf asdfasdfwerwerwerwefwwelkjlkjldkjflaksjdfasdfs sdf sdf sdf sdfs dfsd fsd fsd fsd fsd fdsf dsf";
    }

    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }

        public Address AlternativeAddres { get; set; }
    }

    public enum Place
    {
        Krakow,
        Warszawa,
        Trzebinia
    }
}
