using Colegios.Api.Interfaces;
using Colegios.Api.Models;
using FluentValidation;
using System.Linq;


namespace Colegios.Api.Services.Validators
{
    public class DocenteValidator: AbstractValidator<DocenteModel>
    {
        public DocenteValidator(IDocente servicio, string cnx,bool forUpdate = false)
        {
            RuleFor(x => x.NOMBRE).NotEmpty().WithMessage("La propiedad 'Nombre' es requerida");
            RuleFor(x => x.NOMBRE).MaximumLength(30).WithMessage("La propiedad 'Nombre' debe tener maximo ({MaxLength}) caracteres");
            RuleFor(x => x.DOCUMENTO).NotEmpty().WithMessage("La propiedad 'Documento' es requerida");
            RuleFor(x => x.DOCUMENTO).MaximumLength(20).WithMessage("La propiedad 'Documento' debe tener maximo ({MaxLength}) caracteres");
           // RuleFor(x => x.CORREO).IsValidEmail("Correo");
            RuleFor(x => x).Custom((x, context) =>
            {
                var list = servicio.FindByDocumento(cnx, x.DOCUMENTO).Result;
                bool existe = list.Any();

                string existeError = existe switch
                {
                    _ when (existe && !forUpdate) => $"El documento '{x.DOCUMENTO}' ya existe en la base de datos",
                    _ when (!existe && forUpdate) => $"El documento '{x.DOCUMENTO}' no existe en la base de datos",
                    _ => null

                };
                if (!string.IsNullOrEmpty(existeError))
                    context.AddFailure(existeError);
            });
        }
    }
}
