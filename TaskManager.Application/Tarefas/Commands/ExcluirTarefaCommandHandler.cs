using MediatR;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Tarefas.Commands;

public class ExcluirTarefaCommandHandler : IRequestHandler<ExcluirTarefaCommand, bool>
{
    private readonly ITarefaRepository _repository;

    public ExcluirTarefaCommandHandler(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(ExcluirTarefaCommand request, CancellationToken cancellationToken)
    {
        return await _repository.ExcluirAsync(request.Id, cancellationToken);
    }
}
