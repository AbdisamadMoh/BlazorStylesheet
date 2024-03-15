using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

using StylesheetNET;

using System.Reflection;

namespace BlazorStylesheet
{
    public static class SetUp
    {
        /// <summary>
        /// Registers the Stylesheet service within the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddStylesheet(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            services.AddScoped<Stylesheet>();
        }
    }

    /// <summary>
    /// <inheritdoc cref="CSSSheet"/>
    /// </summary>
    public class Stylesheet : CSSSheet
    {
        internal IJSRuntime _jSRuntime;
        public Stylesheet(IJSRuntime jSRuntime)
        {
            if (jSRuntime == null)
                throw new ArgumentNullException("IJSRuntime");
            _jSRuntime = jSRuntime;
            HideLoader();
        }

        /// <summary>
        /// Creates Styelsheet from the type object passed.
        /// </summary>
        /// <param name="sheet">Stylesheet type object</param>
        /// <exception cref="ArgumentException"></exception>
        public void CreateSheet(Type sheet)
        {
            var expectedConstructor = sheet.GetConstructor( new[] { this.GetType() });
            if (expectedConstructor == null)
            {
              //  throw new ArgumentException($"STYLE TYPE OBJECT PASSED IS MISSING REQUIRED CONSTRUCTOR.\nIT SHOULD HAVE A CONSTRUCTOR LIKE 'public {sheet.Name}(Stylesheet sheet)....)' ");
            }
            var styleClass = Activator.CreateInstance(sheet);
            var properties = GetProperties(styleClass.GetType());
            var sheetProperties = properties.Where(x => x.GetCustomAttributes(typeof(StylesheetProperty), true).Length>0);
            foreach (var prop in sheetProperties)
            {
                prop.SetValue(styleClass, this);
            }
        }
        private IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties( BindingFlags.Public|BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }
        /// <summary>
        /// Compiles the CSS stylesheet and sends to the client. 
        /// </summary>
        /// <returns></returns>
        public async Task Build()
        {
            await Build(true);
        }

        /// <summary>
        /// Compiles the CSS stylesheet and sends to the client. 
        /// </summary>
        /// <param name="minified">Whether to minify the CSS stylesheet</param>
        /// <returns></returns>
        public async Task Build(bool minified)
        {
            var css = this.ToString(minified);
            await _jSRuntime.InvokeVoidAsync("updateStylesheet", css);
        }

        /// <summary>
        /// Hides the loader
        /// </summary>
        public void HideLoader()
        {
            _jSRuntime.InvokeVoidAsync("removeLoader");
        }

    }
}
