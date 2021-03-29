using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cvl.DynamicForms.Test
{
    public class TestPerson
    {
        [Description("Imię")]
        public string Firstname { get; set; }

        [Display(Description ="Nazwisko", GroupName = "Podstawowa", Order =1)]
        public string Surname { get; set; }

        [Display(Description = "Wiek", GroupName = "Podstawowa", Order = 2)]
        public int Age { get; set; }
    }
}
