using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tarefas.Commands;

public record CriarTarefaCommand(string Nome, bool Concluida) : IRequest<TarefaDto>;
