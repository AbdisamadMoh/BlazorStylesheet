using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStylesheet
{
    public class IUserStyle
    {
        Stylesheet sheet;
        public IUserStyle(Stylesheet stylesheet)
        {
            sheet = stylesheet;
        }
    }
}
