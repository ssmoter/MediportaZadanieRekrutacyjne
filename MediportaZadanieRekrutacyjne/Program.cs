using MediportaZadanieRekrutacyjne.Data;
using MediportaZadanieRekrutacyjne.Helper;
using MediportaZadanieRekrutacyjne.Models;
using MediportaZadanieRekrutacyjne.Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddMyService();

builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1",
        new() { Title = "Tag Api", Version = "v1" });
});

builder.Configuration.AddMyConfiguration();
builder.Services.AddScoped(x => new RestSharp.RestClientOptions(builder.Configuration.GetSection("StackOverflowAPI").Value));

var app = builder.Build();


var tagsApi = app.MapGroup("/tag");

tagsApi.MapGet("", async (ITagService _tagService, string order = "desc", string sort = "name", int page = 1) =>
            {
                return await GetTags(order, sort, page, _tagService, false);
            }).WithName("GetTag")
              .WithOpenApi(generatedOperation =>
                        {
                            generatedOperation.Parameters[0].Description = $"Order available: {ListOfEnums.OrderType.desc},{ListOfEnums.OrderType.asc}";
                            generatedOperation.Parameters[1].Description = $"Sorting available: {ListOfEnums.SortType.name},{ListOfEnums.SortType.popular}";
                            generatedOperation.Parameters[2].Description = "Number of page";
                            return generatedOperation;
                        })
                        .Produces<Tag[]>(StatusCodes.Status200OK)
                        .Produces(StatusCodes.Status204NoContent)
                        .Produces<string>(StatusCodes.Status400BadRequest);


tagsApi.MapGet("/refresh", async (ITagService _tagService, string order = "desc", string sort = "name", int page = 1) =>
            {
                return await GetTags(order, sort, page, _tagService, true);
            }).WithName("GetNewTag")
              .WithOpenApi(generatedOperation =>
                        {
                            generatedOperation.Parameters[0].Description = $"Order available: {ListOfEnums.OrderType.desc},{ListOfEnums.OrderType.asc}";
                            generatedOperation.Parameters[1].Description = $"Sorting available: {ListOfEnums.SortType.name},{ListOfEnums.SortType.popular}";
                            generatedOperation.Parameters[2].Description = "Number of page";
                            return generatedOperation;
                        })
                        .Produces<Tag[]>(StatusCodes.Status200OK)
                        .Produces(StatusCodes.Status204NoContent)
                        .Produces<string>(StatusCodes.Status400BadRequest);



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint(
        "/swagger/v1/swagger.json", "v1"));
}

Sqlite.CreateTables();

app.Run();

static async Task<IResult> GetTags(string order, string sort, int page, ITagService _tagService, bool refresh)
{
    (MediportaZadanieRekrutacyjne.Helper.ListOfEnums.OrderType value, string error) orderValidation = MediportaZadanieRekrutacyjne.Helper.TagValidation.OrderValidation(order);
    (MediportaZadanieRekrutacyjne.Helper.ListOfEnums.SortType value, string error) sortValidation = MediportaZadanieRekrutacyjne.Helper.TagValidation.SortValidation(sort);

    if (!string.IsNullOrWhiteSpace(orderValidation.error))
        return Results.BadRequest(orderValidation.error);
    if (!string.IsNullOrWhiteSpace(sortValidation.error))
        return Results.BadRequest(sortValidation.error);

    var result = await _tagService.GetTags(sortValidation.value, orderValidation.value, refresh, page);
    return result;
}