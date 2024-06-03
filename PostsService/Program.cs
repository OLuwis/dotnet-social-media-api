using APICommons;
using PostsService;
using PostsService.Consumers;
using PostsService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase<PostsContext>("Posts.db");
builder.Services.AddMassTransitMQ(builder.Environment.IsProduction(), [typeof(ProfileCreatedConsumer).Assembly, typeof(ProfileUpdatedConsumer).Assembly, typeof(ProfileDeletedConsumer).Assembly]);

builder.Services.AddRepository<PostsRepository>();
builder.Services.AddRepository<ProfilesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    var context = app.Services.GetService<PostsContext>();
    if (context != null) await context.Database.EnsureCreatedAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();