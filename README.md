# TaskManager

API de lista de tarefas construída com **ASP.NET Core Minimal Web API**, seguindo **Clean Architecture** com **CQRS + MediatR**.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)

## Estrutura do projeto

```
TaskManager/
├── TaskManager.Api/             (Presentation) - Minimal API, endpoints, DTOs de entrada
├── TaskManager.Application/     (Use Cases) - Commands, Queries, Handlers, Validação
├── TaskManager.Domain/          (Core) - Entidades e regras de negócio
└── TaskManager.Infrastructure/  - EF Core, DbContext, Repositórios
```

### Camadas

| Projeto                 | Responsabilidade                                          |
| ----------------------- | --------------------------------------------------------- |
| `TaskManager.Domain`    | Entidades (`Tarefa`) e regras de negócio, sem dependências |
| `TaskManager.Application` | Casos de uso (CQRS via MediatR), DTOs, interfaces e validação (FluentValidation) |
| `TaskManager.Infrastructure` | Acesso a dados com EF Core (InMemory) e implementação dos repositórios |
| `TaskManager.Api`       | Endpoints Minimal API e configuração da aplicação         |

## Executando o projeto

```bash
dotnet run --project TaskManager.Api
```

A API ficará disponível em `http://localhost:5095`.

## Endpoints

| Método | Rota                | Descrição                              |
| ------ | ------------------- | -------------------------------------- |
| GET    | `/api/tarefas`      | Lista todas as tarefas                 |
| GET    | `/api/tarefas/{id}` | Retorna uma tarefa pelo id             |
| POST   | `/api/tarefas`      | Cria uma nova tarefa                   |
| PUT    | `/api/tarefas/{id}` | Atualiza nome e status de uma tarefa   |
| DELETE | `/api/tarefas/{id}` | Remove uma tarefa pelo id              |

## Modelo de dados

### Tarefa

| Campo       | Tipo     | Descrição                          |
| ----------- | -------- | ---------------------------------- |
| `id`        | `int`    | Identificador único                |
| `nome`      | `string` | Nome da tarefa                     |
| `concluida` | `bool`   | Indica se a tarefa foi concluída   |

## Exemplos de uso

### Criar tarefa

```http
POST /api/tarefas
Content-Type: application/json

{
  "nome": "Estudar .NET",
  "concluida": false
}
```

### Listar tarefas

```http
GET /api/tarefas
```

### Atualizar tarefa

```http
PUT /api/tarefas/1
Content-Type: application/json

{
  "nome": "Estudar ASP.NET Core",
  "concluida": true
}
```

### Excluir tarefa

```http
DELETE /api/tarefas/1
```

Você também pode usar o arquivo `TaskManager.Api/TaskManager.http` com a extensão *REST Client* (VS Code) para testar os endpoints.

## Observações

- O banco de dados é o **EF Core InMemory**, então os dados são perdidos ao reiniciar a aplicação.
- Para usar um banco real (SQL Server, PostgreSQL etc.), altere a configuração em `TaskManager.Infrastructure/DependencyInjection.cs`.
- A validação de entrada é feita com **FluentValidation**, retornando `400 Bad Request` com a lista de erros.
