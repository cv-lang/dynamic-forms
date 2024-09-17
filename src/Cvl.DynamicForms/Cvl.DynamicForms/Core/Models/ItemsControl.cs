using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models
{
    public class ItemsControl : Control
    {        
        public ItemsControlType Type { get; set; }
        public List<ContentControl> Elements { get; set; } = new List<ContentControl>();
        public List<ItemsControl> Children { get; set; } = new List<ItemsControl>();
        public override string ToString()
        {
            return $"{Name} - {Type}";
        }
    }

    public enum ItemsControlType
    {
        Section, Row, Column, Tabs, Tab, Legend, Table, Grid, Container
    }
}
