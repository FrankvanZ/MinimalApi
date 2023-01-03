using Api.Endpoints.Internal;
using Api.Services;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Api.Endpoints;

public class PlaceholderEndpoint : IEndpoints
{
    private const string SwaggerTag = "Placeholders";
    private const string AcceptsType = "application/json";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("placeholders",
                async (Placeholder placeholder,
                    IPlaceholderService placeholderService,
                    IValidator<Placeholder> validator) =>
                {
                    var validationResult = await validator.ValidateAsync(placeholder);

                    if (!validationResult.IsValid)
                    {
                        return Results.BadRequest(validationResult.Errors);
                    }

                    var exists = await placeholderService.GetById(placeholder.Id);

                    if (exists != null && exists.Equals(placeholder))
                    {
                        return Results.BadRequest("Placeholder with this Name already exists");
                    }

                    var created = await placeholderService.CreateAsync(placeholder);

                    if (!created)
                    {
                        return Results.BadRequest("Something went wrong while trying to create a new artist");
                    }

                    return Results.CreatedAtRoute("GetPlaceholder", new { Id = placeholder.Id }, placeholder);
                })
            .WithName("CreatePlaceholders")
            .Accepts<Placeholder>(AcceptsType)
            .Produces<Placeholder>(201)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(SwaggerTag);

        app.MapGet("placeholders",
                async (IPlaceholderService placeholderService) =>
                {
                    var resultSet = await placeholderService.GetAllAsync();
                    return Results.Ok(resultSet);
                })
            .WithName("GetPlaceholders")
            .Produces<IEnumerable<Placeholder>>(200)
            .WithTags(SwaggerTag);

        app.MapGet("placeholders/{id}",
                async (int id, IPlaceholderService placeholderService) =>
                {
                    var artist = await placeholderService.GetById(id);

                    return artist is not null ? Results.Ok(artist) : Results.NotFound();
                })
            .WithName("GetPlaceholder")
            .Produces<Placeholder>(200)
            .Produces(404)
            .WithTags(SwaggerTag);

        app.MapPut("placeholders/{id}",
                async (int id, Placeholder placeholder, IPlaceholderService placeholderService, IValidator<Placeholder> validator) =>
                {
                    placeholder.Id = id;
                    var validationResult = await validator.ValidateAsync(placeholder);
                    if (!validationResult.IsValid)
                    {
                        return Results.BadRequest(validationResult.Errors);
                    }

                    var updated = await placeholderService.UpdateAsync(placeholder);
                    return updated ? Results.Ok(placeholder) : Results.NotFound();
                })
            .WithName("UpdatePlaceholder")
            .Accepts<Placeholder>(AcceptsType)
            .Produces<Placeholder>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces(404)
            .WithTags(SwaggerTag);

        app.MapDelete("placeholders/{id}",
                async (int id, IPlaceholderService placeholderService) =>
                {
                    var deleted = await placeholderService.DeleteAsync(id);
                    return deleted ? Results.NoContent() : Results.NotFound();
                })
            .WithName("DeletePlaceholder")
            .Produces(204)
            .Produces(404)
            .WithTags(SwaggerTag);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IPlaceholderService, PlaceholderService>();
    }
}