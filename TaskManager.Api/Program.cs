using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskManager.Application;
using TaskManager.Application.DTOs;
using TaskManager.Application.Tarefas.Commands;
using TaskManager.Application.Tarefas.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/api/tarefas", async (ISender sender, CancellationToken cancellationToken) =>
    TypedResults.Ok(await sender.Send(new ListarTarefasQuery(), cancellationToken)));

app.MapGet("/api/tarefas/{id:int}", async Task<Results<Ok<TarefaDto>, NotFound>> (
    int id,
    ISender sender,
    CancellationToken cancellationToken) =>
{
    var tarefa = await sender.Send(new ObterTarefaQuery(id), cancellationToken);

    return tarefa is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(tarefa);
});

app.MapPost("/api/tarefas", async (
    CriarTarefaCommand comando,
    ISender sender,
    CancellationToken cancellationToken) =>
{
    var tarefa = await sender.Send(comando, cancellationToken);

    return TypedResults.Created($"/api/tarefas/{tarefa.Id}", tarefa);
});

app.MapPut("/api/tarefas/{id:int}", async Task<Results<NoContent, NotFound>> (
    int id,
    TarefaInput input,
    ISender sender,
    CancellationToken cancellationToken) =>
{
    var atualizado = await sender.Send(
        new AtualizarTarefaCommand(id, input.Nome, input.Concluida),
        cancellationToken);

    return atualizado
        ? TypedResults.NoContent()
        : TypedResults.NotFound();
});

app.MapDelete("/api/tarefas/{id:int}", async Task<Results<NoContent, NotFound>> (
    int id,
    ISender sender,
    CancellationToken cancellationToken) =>
{
    var excluido = await sender.Send(new ExcluirTarefaCommand(id), cancellationToken);

    return excluido
        ? TypedResults.NoContent()
        : TypedResults.NotFound();
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Tarefas.Any())
    {
        dbContext.Tarefas.Add(new Tarefa { Nome = "allef" });
        dbContext.SaveChanges();
    }
}

app.Run();

public record TarefaInput(string Nome, bool Concluida = false);

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        var errors = validationException.Errors
            .GroupBy(error => error.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.ErrorMessage).ToArray());

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new { errors }, cancellationToken);

        return true;
    }
}
