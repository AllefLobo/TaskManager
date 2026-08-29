using MediatR;

namespace TaskManager.Application.Tarefas.Commands;

public record ExcluirTarefaCommand(int Id) : IRequest<bool>;
