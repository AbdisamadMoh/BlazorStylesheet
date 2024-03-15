using BlazorStylesheet;

using StylesheetNET;
namespace MauiAppBlazor
{
    public class Style
    {
        Stylesheet sheet;
        public Style(Stylesheet stylesheet)
        {
            if (stylesheet == null)
                throw new ArgumentNullException("stylesheet");

            sheet = stylesheet;
            NavBar();
            NavBar_a();
            NavBar_a_Selected();
            NavBar_a_Selected_Hover();

            Animation();
            //These r examples only
            ForMobile();
            ForTablet();
            ForDesktop();

            sheet.Build();
        }

        private void NavBar()
        {
            sheet[".navbar"] = new StylesheetNET.Element()
            {
                Position = PositionOptions.Relative,
                Width = "590px",
                Height = "60px",
                PaddingLeft = "10px",
                PaddingRight = "10px",
                BackgroundColor = "#34495e",
                BorderRadius = "8px",
                FontSize = "0"
            };
        }

        private void NavBar_a()
        {
            sheet[".navbar a"] = new StylesheetNET.Element()
            {
                LineHeight = "50px",
                Height = "100%",
                Width = "100px",
                FontSize = "15px",
                Display = DisplayOptions.InlineBlock,
                Position = PositionOptions.Relative,
                ZIndex = "1",
                TextDecoration = "none",
                TextTransform = TextTransformOptions.Uppercase,
                TextAlign = TextAlignOptions.Center,
                Color = "white",
                Cursor = CursorOptions.Pointer
            };
        }

        private void NavBar_a_Selected()
        {
            sheet[".navbar a.selected"] = new StylesheetNET.Element()
            {
                BackgroundColor = "#17B1EA",
               BorderRadius = "10px"

            };
        }
        private void NavBar_a_Selected_Hover()
        {
            sheet[".navbar a"] = new StylesheetNET.ElementHover()
            {
                BackgroundColor = "#17B1EA",
                BorderRadius = "10px",
                Transition = "border-radius",
                TransitionDuration = ".3s",
                TransitionTimingFunction = TransitionTimingFunctionOptions.EaseIn

            };
        }

        void Animation()
        {
            sheet["h1"] = new StylesheetNET.Element()
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
                ["body"] = new StylesheetNET.Element()
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
                ["body"] = new StylesheetNET.Element()
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
                ["body"] = new StylesheetNET.Element()
                {
                    BackgroundColor = "green"
                }
            };
        }
    }

    
}
