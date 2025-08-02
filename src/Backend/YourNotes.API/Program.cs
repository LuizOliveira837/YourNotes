using YourNotes.API.Filters;
using YourNotes.API.Token;
using YourNotes.Application;
using YourNotes.Persistence;
using YourNotes.Persistence.Autentication.Tokens.Access;
using YourNotes.Persistence.Data.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<YourNotesExceptionFilter>();
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenValue, TokenJwtValue>();
builder.Services.AddPersistenceDependencyInjection(builder.Configuration);
builder.Services.AddApplicationDependencyInjection(builder.Configuration);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrationDatabase.EnsureDatabase(builder.Configuration.GetConnectionString("SqlServer")!);

app.Migrate();
app.Run();


public partial class Program { }