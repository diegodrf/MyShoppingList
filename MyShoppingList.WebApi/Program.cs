using Microsoft.AspNetCore.Mvc;
using MyShoppingList.Application.Commands;
using MyShoppingList.Configurator;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ConfigureDepencencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.MapOpenApi();
    app.MapScalarApiReference("/docs", options =>
    {
        options.WithDarkMode(false);
    });
}

//app.UseHttpsRedirection();

var version1 = app.MapGroup("/v1");

var groupRoutes = version1.MapGroup("/group");
groupRoutes.MapPost("/", CreateGroupAsync);
groupRoutes.MapGet("/", ListAllGroupsAsync);
groupRoutes.MapGet("/{id}", GetGroupByIdAsync);
groupRoutes.MapPost("/{groupId}/item", CreateItemAsync);
groupRoutes.MapPost("/{groupId}/item/{itemId}", AddItemAsync);
groupRoutes.MapDelete("/{groupId}/item/{itemId}", RemoveItemAsync);
groupRoutes.MapPost("/{groupId}/item/{itemId}/complete", CompleteItemAsync);
groupRoutes.MapPost("/{groupId}/item/{itemId}/uncomplete", UncompleteItemAsync);

var itemRoutes = version1.MapGroup("/item");


app.Run();

static async Task<IResult> CreateGroupAsync(
    CreateGroupCommand command,
    CreateGroupHandler handler,
    CancellationToken cancellationToken)
{
    var group = await handler.HandleAsync(command, cancellationToken);
    return TypedResults.Created($"/v1/group/{group.Id}", group);
}

static async Task<IResult> ListAllGroupsAsync(
    GetAllGroupsHandler handler,
    CancellationToken cancellationToken)
{
    var groups = await handler.HandleAsync(new GetAllGroupsCommand(), cancellationToken);
    return TypedResults.Ok(groups);
}

static async Task<IResult> GetGroupByIdAsync(
    int id,
    GetGroupByIdHandler handler,
    CancellationToken cancellationToken)
{
    var command = new GetGroupByIdCommand { Id = id };
    var result = await handler.HandleAsync(command, cancellationToken);
    if (result is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
}

static async Task<IResult> AddItemAsync(
    int groupId,
    int itemId,
    AddItemInGroupHandler handler,
    CancellationToken cancellationToken)
{
    var command = new AddItemInGroupCommand { GroupId = groupId, ItemId = itemId };
    var result = await handler.HandleAsync(command, cancellationToken);
    if (result is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
}

static async Task<IResult> RemoveItemAsync(
    int groupId,
    int itemId,
    RemoveItemFromGroupHandler handler,
    CancellationToken cancellationToken)
{
    var command = new RemoveItemFromGroupCommand { GroupId = groupId, ItemId = itemId };
    var result = await handler.HandleAsync(command, cancellationToken);
    if (result is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
}

static async Task<IResult> CompleteItemAsync(
    int groupId,
    int itemId,
    CompleteItemHandler handler,
    CancellationToken cancellationToken)
{
    var command = new CompleteItemCommand { GroupId = groupId, ItemId = itemId };
    var result = await handler.HandleAsync(command, cancellationToken);
    if (result is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
}

static async Task<IResult> UncompleteItemAsync(
    int groupId,
    int itemId,
    UncompleteItemHandler handler,
    CancellationToken cancellationToken)
{
    var command = new UncompleteItemCommand { GroupId = groupId, ItemId = itemId };
    var result = await handler.HandleAsync(command, cancellationToken);
    if (result is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
}

static async Task<IResult> CreateItemAsync(
    [FromRoute] int groupId,
    [FromBody] string name,
    CreateItemHandler handler,
    CancellationToken cancellationToken)
{
    var command = new CreateItemCommand { GroupId = groupId, Name = name };
    var result = await handler.HandleAsync(command, cancellationToken);
    if (result is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(result);
}