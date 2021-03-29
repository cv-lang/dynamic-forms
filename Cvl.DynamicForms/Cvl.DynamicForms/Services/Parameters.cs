using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Services
{
    public class Parameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Parameters
    {
        private IEnumerable<object> enumerable;

        public Parameters(IEnumerable<Parameter> enumerable)
        {
            this.enumerable = enumerable;
        }
    }
}
