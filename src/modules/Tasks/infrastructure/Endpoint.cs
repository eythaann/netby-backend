using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Application;
using Tasks.Domain;

namespace Tasks
{
    public class TasksEndpoint : BaseEndpoint
    {
        private TaskRepository repository = new SqlServerTasksRepository(Env.Config.GetConnectionString());

        public IResult GetAll(ClaimsPrincipal claims)
        {
            var userId = claims.FindFirst("userId")?.Value;
            if (userId == null)
            {
                return Results.Unauthorized();
            }
            return Results.Json(repository.AllFromUser(userId));
        }

        public IResult GetOne(ClaimsPrincipal claims, string id)
        {
            var userId = claims.FindFirst("userId")?.Value;
            if (userId == null)
            {
                return Results.Unauthorized();
            }

            var task = repository.Search(id);
            if (task == null)
            {
                return Results.NotFound();
            }

            if (task.UserId != userId)
            {
                return Results.Unauthorized();
            }

            return Results.Json(task);
        }

        public IResult CreateOne(ClaimsPrincipal claims)
        {
            var userId = claims.FindFirst("userId")?.Value;
            if (userId == null)
            {
                return Results.Unauthorized();
            }
            repository.Add(new TodoTask(userId));
            return Results.Ok();
        }

        public IResult DeleteOne(ClaimsPrincipal claims, string id)
        {
            var userId = claims.FindFirst("userId")?.Value;
            if (userId == null)
            {
                return Results.Unauthorized();
            }

            var task = repository.Search(id);
            if (task == null)
            {
                return Results.NotFound();
            }

            if (task.UserId != userId)
            {
                return Results.Unauthorized();
            }

            repository.Delete(id);
            return Results.Ok();
        }

        public IResult UpdateOne(ClaimsPrincipal claims, string id, [FromBody] UpdateTaskPayload payload)
        {
            var isValid = MiniValidation.MiniValidator.TryValidate(payload, out var errors);
            if (!isValid)
            {
                return Results.ValidationProblem(errors);
            }

            var userId = claims.FindFirst("userId")?.Value;
            if (userId == null)
            {
                return Results.Unauthorized();
            }

            var task = repository.Search(id);
            if (task == null)
            {
                return Results.NotFound();
            }

            if (task.UserId != userId)
            {
                return Results.Unauthorized();
            }

            repository.Update(id, payload);
            return Results.Ok();
        }

        public override void Register(WebApplication app)
        {
            app.MapGet($"{root}/tasks", GetAll).RequireAuthorization();

            app.MapPost($"{root}/task", CreateOne).RequireAuthorization();
            app.MapGet($"{root}/task/{{id}}", GetOne).RequireAuthorization();
            app.MapPut($"{root}/task/{{id}}", UpdateOne).RequireAuthorization();
            app.MapDelete($"{root}/task/{{id}}", DeleteOne).RequireAuthorization();
        }

        public TasksEndpoint() : base(null) { }
        public TasksEndpoint(string path) : base(path) { }
    }
}