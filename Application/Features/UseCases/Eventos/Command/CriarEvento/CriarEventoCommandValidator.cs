using FluentValidation;

namespace Application.Features.UseCases.Pedidos.Command.CriarPedido
{
    public class CriarEventoCommandValidator : AbstractValidator<CriarEventoCommand>
    {
        public CriarEventoCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do evento é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do cliente não pode exceder 100 caracteres.");
            RuleFor(x => x.DataEvento)
                .NotEmpty().WithMessage("a data do evento é obrigatório.");
        }
    }
}
