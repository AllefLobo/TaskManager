using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tarefas.Queries;

public record ObterTarefaQuery(int Id) : IRequest<TarefaDto?>;
