
namespace Colegios.Api.Extensions
{
    public static class ModelsExtension
    {
        /// <summary>
        /// Convierte a mayusculas los datos tipo string de un modelo generico
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public static void DataToUpper<T>(this T model)
        {
            foreach (var prop in model.GetType().GetProperties())
            {
                var stringValue = prop.GetValue(model)?.ToString();
                if (prop.PropertyType != typeof(string) || string.IsNullOrEmpty(stringValue)) continue;

                prop.SetValue(model, stringValue.RemoveMultiWhiteSpaces().ToUpper());
            }
        }
    }
}
