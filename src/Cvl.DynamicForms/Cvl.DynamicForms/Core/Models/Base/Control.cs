using Cvl.DynamicForms.Core.ControlDescriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Base
{
    /// <summary>
    /// Kontrolka bazowa dla wszystkich kontrolek
    /// </summary>
    public abstract class Control
    {        
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
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
