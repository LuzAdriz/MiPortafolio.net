using FluentValidation;


namespace Colegios.Api.Extensions
{
    public static class ValidatorRulerBuilderExtensions
    {
        /// <summary>
        /// Valida que un string contenga solo numeros como regla de fluent validator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IRuleBuilder<T, string> StringOnlyNumber<T>(this IRuleBuilder<T, string> ruleBuilder, string propertyName)
        {
            return ruleBuilder.Matches(@"^-?\\d*(\\.\\d+)?$").WithMessage($"La propiedad '{propertyName}' debe contener unicamente valores numericos");
        }
        /// <summary>
        /// Valida que un string tenga formato email regla de fluent validator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IRuleBuilder<T, string> IsValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder, string propertyName)
        {
            return ruleBuilder.Custom((x, context) =>
            {
                if (x.IsValidEmail() && !string.IsNullOrEmpty(x))
                    context.AddFailure($"La propiedad '{propertyName}' debe indicar un email valido");
            });
        }
    }
}
