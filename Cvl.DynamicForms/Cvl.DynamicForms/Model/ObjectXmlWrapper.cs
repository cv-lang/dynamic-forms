using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Model
{
    public class ObjectXmlWrapper
    {
        public string Xml { get; set; }

        public ObjectXmlWrapper(string xml)
        {
            this.Xml = xml;
        }
    }
}
