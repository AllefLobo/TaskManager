using MediatR;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Application.Tarefas.Commands;

public class AtualizarTarefaCommandHandler : IRequestHandler<AtualizarTarefaCommand, bool>
{
    private readonly ITarefaRepository _repository;

    public AtualizarTarefaCommandHandler(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AtualizarTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await _repository.ObterPorIdAsync(request.Id, cancellationToken);

        if (tarefa is null)
        {
            return false;
        }

        tarefa.Nome = request.Nome;
        tarefa.Concluida = request.Concluida;

        return await _repository.AtualizarAsync(tarefa, cancellationToken);
    }
}
