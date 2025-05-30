namespace MyApp.Application.Validators
{
    using FluentValidation;
    using MyApp.Application.Interfaces;

    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(x => x.NombreTicket)
                .NotEmpty().WithMessage("NombreTicket is required")
                .MaximumLength(20).WithMessage("NombreTicket must not exceed 20 characters");

            RuleFor(x => x.DesignTicket)
                .NotEmpty().WithMessage("DesignTicket is required");
        }
    }
}