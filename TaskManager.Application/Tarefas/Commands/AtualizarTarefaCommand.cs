using MediatR;

namespace TaskManager.Application.Tarefas.Commands;

public record AtualizarTarefaCommand(int Id, string Nome, bool Concluida) : IRequest<bool>;
