using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.SharpSerializer.Model
{
    public class Reference : BaseObject
    {
        public override void Render(IRenderer renderer)
        {
            renderer.RenderSimpleProperty("Reference", "Reference", Id.ToString());
        }
    }
}
