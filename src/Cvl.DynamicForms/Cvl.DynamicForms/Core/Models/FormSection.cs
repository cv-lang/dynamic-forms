using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models
{
    public class FormSection
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public FormSectionType SectionType { get; set; }
        public List<FormElement> Elements { get; set; } = new List<FormElement>();
        public List<FormSection> Children { get; set; } = new List<FormSection>();

        public override string ToString()
        {
            return $"{Name} - {SectionType}";
        }
    }

    public enum FormSectionType
    {
        Section, Row, Column, Tabs, Tab, Legend, Table
    }
}
