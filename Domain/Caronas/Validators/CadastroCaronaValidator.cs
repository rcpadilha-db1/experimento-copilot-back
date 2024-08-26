using Crosscutting.Requests;
using Domain._Base.Validators;
using Domain.Caronas.interfaces;
using FluentValidation;

namespace Domain.Caronas.Validators;

public class CadastroCaronaValidator : BaseValidator<CaronaRequest>, ICadastroCaronaValidator
{
    public CadastroCaronaValidator()
    {
        RuleFor(c => c.VeiculoId).NotEmpty();
        
        RuleFor(c => c.UsuarioId).NotEmpty();
        
        RuleFor(c => c.Data).NotEmpty().GreaterThanOrEqualTo(DateTime.Now.Date);
    }
    
}