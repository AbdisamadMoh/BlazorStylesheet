namespace BlazorStylesheet
{
    /// <summary>
    /// Indictates that this class is for Stylesheet
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class StylesheetClass : Attribute
    {
    }
    /// <summary>
    /// Tells BlazorStylesheet to reference this property to the main stylesheet
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StylesheetProperty : Attribute
    {
    }
    /// <summary>
    /// Tells BlazorStylesheet that this method holds stylesheet contents and should be executed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class StylesheetMethod : Attribute
    {
    }
}
