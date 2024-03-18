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
        private readonly Refactory _factory = new Refactory();
        /// <summary>
        /// Creates Styelsheet from the stlylesheet class object passed. The type should be a valid stylehseet class that have atleast required attributes.
        /// </summary>
        /// <param name="sheet">Stylesheet type object</param>
        /// <exception cref="ArgumentException"></exception>
        public void CreateSheet(Type sheet)
        {
          _factory.CreateStylesheet(sheet, this);
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
