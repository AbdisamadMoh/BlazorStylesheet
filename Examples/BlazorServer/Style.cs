using BlazorStylesheet;

using StylesheetNET;
namespace BlazorServer
{
    public class Style
    {
        Stylesheet sheet;
        public Style(Stylesheet stylesheet)
        {
            if (stylesheet == null)
                throw new ArgumentNullException("stylesheet");

            sheet = stylesheet;

        }
        public void CreateCss()
        {

            Animation();
            //These r examples only
            ForMobile();
            ForTablet();
            ForDesktop();
        }
        void Animation()
        {
            sheet["h1"] = new Element()
            {
                AnimationName = "pulse",
                AnimationDuration = "2s",
                AnimationIterationCount = AnimationIterationCountOptions.Infinite
            };

            sheet[AtRuleType.Keyframes] = new Keyframes("pulse")
            {
                ["from"] = new Keyframe()
                {
                    Opacity = "1.0"
                },
                ["to"] = new Keyframe()
                {
                    Opacity = "0"
                }
            };
        }
        //Media Query for Mobile Devices
        // @media (max-width: 480px) 
        void ForMobile()
        {
            sheet[AtRuleType.MediaQuery] = new MediaQuery(new AtRule().MaxWidth("480px"))
            {
                ["body"] = new Element()
                {
                    BackgroundColor = "red"
                }
            };
        }
        // Media Query for low resolution  Tablets, Ipads
        // @media (min-width: 481px) and (max-width: 767px)
        void ForTablet()
        {
            sheet[AtRuleType.MediaQuery] = new MediaQuery(new AtRule().MinWidth("481px").And.MaxWidth("767px"))
            {
                ["body"] = new Element()
                {
                    BackgroundColor = "yellow"
                }
            };
        }

        // Media Query for Laptops and Desktops
        // @media (min-width: 1025px) and (max-width: 1280px)
        void ForDesktop()
        {
            sheet[AtRuleType.MediaQuery] = new MediaQuery(new AtRule().MinWidth("1025px").And.MaxWidth("1280px"))
            {
                ["body"] = new Element()
                {
                    BackgroundColor = "green"
                }
            };
        }
    }
}
