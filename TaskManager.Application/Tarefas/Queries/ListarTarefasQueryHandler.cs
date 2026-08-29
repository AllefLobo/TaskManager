using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tarefas.Queries;

public class ListarTarefasQueryHandler : IRequestHandler<ListarTarefasQuery, List<TarefaDto>>
{
    private readonly ITarefaRepository _repository;

    public ListarTarefasQueryHandler(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TarefaDto>> Handle(ListarTarefasQuery request, CancellationToken cancellationToken)
    {
        var tarefas = await _repository.ListarAsync(cancellationToken);

        return tarefas
            .Select(t => new TarefaDto(t.Id, t.Nome, t.Concluida))
            .ToList();
    }
}
