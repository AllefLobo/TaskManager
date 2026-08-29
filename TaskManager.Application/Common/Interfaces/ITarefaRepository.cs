using TaskManager.Domain.Entities;

namespace TaskManager.Application.Common.Interfaces;

public interface ITarefaRepository
{
    Task<List<Tarefa>> ListarAsync(CancellationToken cancellationToken);
    Task<Tarefa?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
    Task<Tarefa> AddAsync(Tarefa tarefa, CancellationToken cancellationToken);
    Task<bool> AtualizarAsync(Tarefa tarefa, CancellationToken cancellationToken);
    Task<bool> ExcluirAsync(int id, CancellationToken cancellationToken);
}
