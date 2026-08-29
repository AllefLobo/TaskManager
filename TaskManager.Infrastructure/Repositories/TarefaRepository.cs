using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tarefa>> ListarAsync(CancellationToken cancellationToken)
    {
        return await _context.Tarefas
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Tarefa?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Tarefas
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Tarefa> AddAsync(Tarefa tarefa, CancellationToken cancellationToken)
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync(cancellationToken);

        return tarefa;
    }

    public async Task<bool> AtualizarAsync(Tarefa tarefa, CancellationToken cancellationToken)
    {
        _context.Tarefas.Update(tarefa);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> ExcluirAsync(int id, CancellationToken cancellationToken)
    {
        var tarefa = await _context.Tarefas.FindAsync(id, cancellationToken);

        if (tarefa is null)
        {
            return false;
        }

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
