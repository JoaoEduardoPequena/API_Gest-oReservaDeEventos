using FluentValidation;

namespace Application.Features.UseCases.Pedidos.Command.CriarPedido
{
    public class CriarPedidoCommandValidator : AbstractValidator<CriarPedidoCommand>
    {
        public CriarPedidoCommandValidator()
        {
            RuleFor(x => x.NomeCliente)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do cliente não pode exceder 100 caracteres.");
            RuleFor(x => x.EmailCliente)
                .NotEmpty().WithMessage("O email do cliente é obrigatório.")
                .EmailAddress().WithMessage("O email do cliente deve ser um endereço de email válido.");
            RuleFor(x => x.UserFaceId)
                .NotEmpty().WithMessage("O Id do Faceio não foi gerado é obrigatório.");
        }
    }
}
