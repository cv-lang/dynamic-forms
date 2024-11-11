using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Base
{
    /// <summary>
    /// Kontrolka bazowa dla wszystkich
    /// </summary>
    public abstract class Control
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? DataSource { get; set; }

        public ControlType Type { get; set; }
    }

    public enum ControlType
    {
        //kontenery
        Section, Row, Column, Tabs, Tab, Legend, Table, Grid, Container,

        //proste kontrolki
        Auto,
        Text,
        Date,
        Checkbox,
        Button,
        Combo,
        Icon,
        Info,
        Currency
    }
}
