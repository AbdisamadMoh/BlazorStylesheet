namespace BlazorStylesheet
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StylesheetClass : Attribute
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
