using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

using StylesheetNET;

namespace BlazorStylesheet
{
    public static class SetUp
    {

        public static void AddStylesheet(this IServiceCollection services)
        {
            if(services == null)
                throw new ArgumentNullException(nameof(services));
            services.AddScoped<Stylesheet>();
        }
    }
    public class Stylesheet : CSSSheet
    {
        internal IJSRuntime _jSRuntime;
        public Stylesheet(IJSRuntime jSRuntime)
        {
            if (jSRuntime == null)
                 throw new ArgumentNullException("IJSRuntime");
            _jSRuntime = jSRuntime;
        }
        public async Task Build()
        {
             await Build(true);
        }
        public async Task Build(bool minified)
        {
            var css = this.ToString(true);
            await _jSRuntime.InvokeVoidAsync("updateStylesheet", css);
        }

    }
}
