using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tarefas.Queries;

public record ListarTarefasQuery : IRequest<List<TarefaDto>>;
