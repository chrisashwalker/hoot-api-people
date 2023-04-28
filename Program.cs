using hoot_api_people.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; // Return 406 if response not in a format accepted by client, e.g JSON, XML.
})
.AddNewtonsoftJson() // Required for Json PATCH.
.AddXmlDataContractSerializerFormatters(); // Add support for XML responses.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PeopleContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("HootDbPostgres")));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<PeopleContext>();
PeopleInitialiser.Initialise(context);

app.Run();
