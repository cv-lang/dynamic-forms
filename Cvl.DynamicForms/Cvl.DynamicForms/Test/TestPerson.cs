using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cvl.DynamicForms.Test
{
    [Serializable]
    [XmlRoot("Properties")]
    public class TestPerson
    {
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

    }

    public enum Place
    {
        Krakow,
        Warszawa,
        Trzebinia
    }
}
