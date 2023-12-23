using System;
using System.Text.RegularExpressions;

namespace Colegios.Api.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Quita los espacios en blanco entre palabras cuanto tiene mas de un espacio
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string RemoveMultiWhiteSpaces(this string word)
        {
            var sanitize = string.Join(" ", word.Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries));

            return sanitize?.Trim();
        }
        /// <summary>
        /// Valida si un string tiene formato valido de email
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string value)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(value);

            return match.Success ? true : false;
          
        }
    }
    
}
