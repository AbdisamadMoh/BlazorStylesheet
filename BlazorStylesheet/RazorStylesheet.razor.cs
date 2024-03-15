using Microsoft.AspNetCore.Components;

namespace BlazorStylesheet
{
    /// <summary>
    /// jjjj
    /// </summary>
    public partial class RazorStylesheet
    {
        public RazorStylesheet()
        {

        }
        [Inject]
        public Stylesheet sheet
        {
            get; set;
        }
        [Parameter]
        public Type StyleSheet
        {
            get; set;
        }
        [Parameter]
        public RenderFragment ChildContent
        {
            get; set;
        }
        protected override void OnAfterRender(bool firstRender)
        {
            var attr = StyleAtrributes();
            if(attr.Count>0)
            {
                foreach(var style in attr)
                {
                    sheet.CreateSheet(style);
                }
                sheet.Build();
            }
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                if (StyleSheet != null)
                {
                  
                    //    var ass =   Assembly.GetEntryAssembly();
                  //  sheet.CreateSheet(StyleSheet);
                }
            }
        }
        private List<Type> StyleAtrributes()
        {
            var classesWithMyAttribute =
            from a in AppDomain.CurrentDomain.GetAssemblies()
            from t in a.GetTypes()
            let attributes = t.GetCustomAttributes(typeof(StylesheetClass), true)
            where attributes != null && attributes.Length > 0
            select new
            {
               Type = t,
                Attributes = attributes.Cast<StylesheetClass>(),

            };
            List<Type> ty = new List<Type>();
            foreach (var attr in classesWithMyAttribute)
            {
                ty.Add(attr.Type);
            }
            return ty;
        }
    }
}
