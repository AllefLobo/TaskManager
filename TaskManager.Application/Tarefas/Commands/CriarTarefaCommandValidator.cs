using FluentValidation;
using TaskManager.Application.Tarefas.Commands;

namespace TaskManager.Application.Tarefas.Commands;

public class CriarTarefaCommandValidator : AbstractValidator<CriarTarefaCommand>
{
    public CriarTarefaCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O nome da tarefa é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O nome da tarefa deve ter no máximo 100 caracteres.");
    }
}
