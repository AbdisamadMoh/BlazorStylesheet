using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStylesheet
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StylesheetClass:Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class StylesheetProperty : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class StylesheetMethod : Attribute
    {
    }
}
