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
        public RenderFragment ChildContent
        {
            get; set;
        }

        [Parameter]
        public bool Minify
        {
            get; set;
        } = true;
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                Refactory refactory = new Refactory();

                var attr = refactory.GetStyleObjects();
                if (attr.Count > 0)
                {
                    foreach (var style in attr)
                    {
                        sheet.CreateSheet(style);
                    }
                    sheet.Build(Minify);
                }
            }
            base.OnAfterRender(firstRender);

        }
        
    }
}
