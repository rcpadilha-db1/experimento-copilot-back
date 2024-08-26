using Crosscutting.Requests;
using FluentValidation;

namespace Domain.Caronas.interfaces;

public interface ICadastroCaronaValidator : IValidator<CaronaRequest>
{
    
}