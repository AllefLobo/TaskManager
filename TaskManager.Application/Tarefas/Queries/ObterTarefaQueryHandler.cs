using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tarefas.Queries;

public class ObterTarefaQueryHandler : IRequestHandler<ObterTarefaQuery, TarefaDto?>
{
    private readonly ITarefaRepository _repository;

    public ObterTarefaQueryHandler(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<TarefaDto?> Handle(ObterTarefaQuery request, CancellationToken cancellationToken)
    {
        var tarefa = await _repository.ObterPorIdAsync(request.Id, cancellationToken);

        return tarefa is null
            ? null
            : new TarefaDto(tarefa.Id, tarefa.Nome, tarefa.Concluida);
    }
}
