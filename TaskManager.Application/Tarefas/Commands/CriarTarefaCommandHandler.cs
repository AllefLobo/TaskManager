using MediatR;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Tarefas.Commands;

public class CriarTarefaCommandHandler : IRequestHandler<CriarTarefaCommand, TarefaDto>
{
    private readonly ITarefaRepository _repository;

    public CriarTarefaCommandHandler(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<TarefaDto> Handle(CriarTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = new Tarefa
        {
            Nome = request.Nome,
            Concluida = request.Concluida
        };

        tarefa = await _repository.AddAsync(tarefa, cancellationToken);

        return new TarefaDto(tarefa.Id, tarefa.Nome, tarefa.Concluida);
    }
}
