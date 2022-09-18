using Dapper.Contrib.Extensions;
using TarefasAPI.Data;
using static TarefasAPI.Data.TarefaContext;

namespace TarefasAPI.EndPoints
{
    public static class TarefasEndpoints
    {
        public static void MapTarefasEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => $"Bem-vindo a API Tarefas - {DateTime.Now}");

            app.MapGet("/tarefas", async (GetConnection connectionGetter) =>
            {
                using (var con = await connectionGetter())
                {
                    var result = con.GetAll<Tarefas>().ToList();
                    if (result is null)
                        return Results.NotFound();

                    return Results.Ok(result);
                }
            });

            app.MapGet("/tarefas/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using (var con = await connectionGetter())
                {
                    var result = con.Get<Tarefas>(id);
                    if (result is null)
                        return Results.NotFound();

                    return Results.Ok(result);
                }
            });

            app.MapPost("/tarefas", async (GetConnection connectionGetter, Tarefas tarefa) =>
            {
                using var con = await connectionGetter();
                var id = con.Insert(tarefa);
                return Results.Created($"/tarefas/{id}", tarefa);
            });

            app.MapPut("/tarefas", async (GetConnection connectionGetter, Tarefas tarefa) =>
            {
                using var con = await connectionGetter();
                var id = con.Update(tarefa);
                return Results.Ok();
            });

            app.MapDelete("/tarefas/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();

                var deleted = con.Get<Tarefas>(id);
                if (deleted is null)
                    return Results.NotFound();

                con.Delete(deleted);
                return Results.Ok(deleted);
            });


        }
    }
}
