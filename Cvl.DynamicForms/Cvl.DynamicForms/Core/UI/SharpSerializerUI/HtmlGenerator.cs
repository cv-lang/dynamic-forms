using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.DynamicForms.Core.SharpSerializer.Model;

namespace Cvl.DynamicForms.Core.UI.SharpSerializerUI
{
    internal class HtmlElement
    {
        public HtmlElement(string start, string end)
        {
            Start = start;
            End = end;
        }

        public string Start { get; set; }
        public string End { get; set; }
    }
    internal class HtmlGenerator
    {
        private string GetLable(int id, string title)
        {
            return $"<label for='input{id}' class='col-sm-2 col-form-label'>{title}</label>";
        }

        private string GetSimpleElementRow(string label,string control)
        {
            var html = $@"<div class='row mb-3'>
	<label for='inputEmail3' class='col-sm-2 col-form-label'>Email</label>
	<div class='col-sm-10'>
	  <input type='email' class='form-control' id='inputEmail3'>
	</div>
</div>";

            return html;
        }

        public HtmlElement GenerateHtml(Null nNull)
        {
            return new HtmlElement("<div class='row mb-3'><label for='inputEmail3' class='col-sm-2 col-form-label'>Email</label><div class='col-sm-10'>", "");
        }
    }
}
