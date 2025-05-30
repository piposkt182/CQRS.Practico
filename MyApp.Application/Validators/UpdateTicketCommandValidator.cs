using FluentValidation;
using MyApp.Application.Interfaces; // This references the namespace where UpdateTicketCommand is defined

namespace MyApp.Application.Validators
{
    public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.NombreTicket)
                .NotEmpty().WithMessage("NombreTicket must not be empty.")
                .MaximumLength(100).WithMessage("NombreTicket must not exceed 100 characters.");

            RuleFor(x => x.DesignTicket)
                .NotEmpty().WithMessage("DesignTicket must not be empty.");

            // For boolean properties like Timbrado, NotNull can be used if the property is nullable,
            // but since bool is a value type and defaults to false,
            // specific validation (e.g., MustBeTrue or MustBeFalse) is only needed if 'false' is an invalid state.
            // The requirement "Timbrado must be a boolean value" is inherently met by its type.
            // No explicit rule is added for Timbrado itself unless specific true/false validation is needed.
        }
    }
}
