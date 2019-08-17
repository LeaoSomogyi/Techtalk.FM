using FluentValidation;
using System;

namespace Techtalk.FM.Domain.DTOs.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            #region "  Not Null / Not Empty  "

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe o título do livro")
                .WithErrorCode("400");

            RuleFor(x => x.Subtitle)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe o subtítulo do livro")
                .WithErrorCode("400");

            RuleFor(x => x.Author)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe o autor do livro")
                .WithErrorCode("400");

            RuleFor(x => x.PublishDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe a data de publicação do livro")
                .WithErrorCode("400");

            RuleFor(x => x.PageNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe o número de páginas do livro")
                .WithErrorCode("400");

            RuleFor(x => x.PublishingHouse)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe a editora do livro")
                .WithErrorCode("400");

            RuleFor(x => x.ISBN)
                .NotNull()
                .NotEmpty()
                .WithMessage("Por favor, informe o ISBN do livro")
                .WithErrorCode("400");

            #endregion

            #region "  Max Length / Min Length  "

            RuleFor(x => x.Title)
                .MaximumLength(250)
                .WithMessage("O campo title deve conter no máximo 250 caracteres")
                .WithErrorCode("400");

            RuleFor(x => x.Subtitle)
                .MaximumLength(250)
                .WithMessage("O campo subtitle deve conter no máximo 250 caracteres")
                .WithErrorCode("400");

            RuleFor(x => x.Author)
                .MaximumLength(150)
                .WithMessage("O campo author deve conter no máximo 150 caracteres")
                .WithErrorCode("400");

            RuleFor(x => x.ISBN)
                .MinimumLength(10)
                .MaximumLength(13)
                .WithMessage("O campo isbn deve conter no máximo 13 e no mínimo 10 caracteres")
                .WithErrorCode("400");

            #endregion

            #region "  Custom Validators  "

            RuleFor(x => x.PublishDate).Must(predicate: (book, date) =>
            {
                return !date.Equals(DateTime.MinValue);
            })
            .WithMessage("A data de publicação está em um formato inválido.")
            .WithErrorCode("400");

            #endregion
        }
    }
}
