using System.Reflection;

namespace BlazorStylesheet
{
    internal class Refactory
    {
        public List<Type> GetStyleObjects()
        {
            var styleObjects =
            from app in AppDomain.CurrentDomain.GetAssemblies()
            from type in app.GetTypes()
            let attributes = type.GetCustomAttributes(typeof(StylesheetClass), true)
            where attributes != null && attributes.Length > 0
            select new
            {
                Type = type,
                Attributes = attributes.Cast<StylesheetClass>(),

            };
            List<Type> ty = new List<Type>();
            foreach (var attr in styleObjects)
            {
                ty.Add(attr.Type);
            }
            return ty;
        }
        public void CreateStylesheet(Type sheet, object origin)
        {
            var constructor = sheet.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new ArgumentException($"STYLE CLASS IS MISSING REQUIRED CONSTRUCTOR.\nIT SHOULD HAVE A PARAMETERLESS CONSTRUCTOR LIKE 'public {sheet.Name}(){{}}' ");
            }
            var styleClass = Activator.CreateInstance(sheet);
            this.SetValues(styleClass, origin);
            this.InvokeMthods(styleClass);
        }
        public void InvokeMthods(object obj)
        {
            var methods = GetMethods(obj.GetType());
            var sheetMethods = methods.Where(x => x.GetCustomAttributes(typeof(StylesheetMethod), true).Length > 0);
            foreach (var method in sheetMethods)
            {
                method.Invoke(obj, null);
            }
        }
        public void SetValues(object obj, object origin)
        {
            var properties = GetProperties(obj.GetType());
            var sheetProperties = properties.Where(x => x.GetCustomAttributes(typeof(StylesheetProperty), true).Length > 0);
            foreach (var prop in sheetProperties)
            {
                prop.SetValue(obj, origin);
            }
        }
        public IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }
        public IEnumerable<MethodInfo> GetMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }
    }
}
