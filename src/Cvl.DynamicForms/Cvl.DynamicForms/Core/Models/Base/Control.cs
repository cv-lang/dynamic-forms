using Cvl.DynamicForms.Core.ControlDescriptions;
using Cvl.DynamicForms.Core.Models.ContentControls;
using Cvl.DynamicForms.Core.Models.ItemsControls;
using Cvl.DynamicForms.Core.Models.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models.Base
{
    /// <summary>
    /// Kontrolka bazowa dla wszystkich kontrolek
    /// </summary>    
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(ContainerControl), "ContainerControl")]
    [JsonDerivedType(typeof(ContentControl), "ContentControl")]
    [JsonDerivedType(typeof(AutoComplete), "AutoComplete")]
    [JsonDerivedType(typeof(Body), "Body")]
    [JsonDerivedType(typeof(Button), "Button")]
    [JsonDerivedType(typeof(ComboBox), "ComboBox")]
    [JsonDerivedType(typeof(Currency), "Currency")]
    [JsonDerivedType(typeof(DatePicker), "DatePicker")]
    [JsonDerivedType(typeof(Image), "Image")]
    [JsonDerivedType(typeof(Info), "Info")]
    [JsonDerivedType(typeof(Link), "Link")]
    [JsonDerivedType(typeof(TextBox), "TextBox")]
    [JsonDerivedType(typeof(Table), "Table")]
    [JsonDerivedType(typeof(Column), "Column")]
    [JsonDerivedType(typeof(Footer), "Footer")]
    [JsonDerivedType(typeof(Grid), "Grid")]
    [JsonDerivedType(typeof(Header), "Header")]
    [JsonDerivedType(typeof(Layout), "Layout")]
    [JsonDerivedType(typeof(Legend), "Legend")]
    [JsonDerivedType(typeof(Row), "Row")]
    [JsonDerivedType(typeof(Section), "Section")]
    [JsonDerivedType(typeof(Sidebar), "Sidebar")]
    [JsonDerivedType(typeof(Stack), "Stack")]
    [JsonDerivedType(typeof(Tab), "Tab")]
    [JsonDerivedType(typeof(Tabs), "Tabs")]
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
        Column, Footer, Grid, Header, Layout, Legend, Row, Section, Sidebar, Stack, Tab, Tabs, Table,

        //proste kontrolki
        Auto,
        AutoComplete,
        Body,
        Button,
        ComboBox,
        Currency,
        DatePicker,
        Image,
        Info,
        Link,
        TextBox,
    }
}
