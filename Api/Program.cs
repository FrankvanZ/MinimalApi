using Api.Endpoints.Internal;
using FluentValidation;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddEndpoints<Program>(builder.Configuration);

builder.Configuration.AddJsonFile("appsettings.Development.json", true, true);

// builder.Services.AddScoped<DbContext>(provider => provider.GetService<DataContext>());
// builder.Services.AddUnitOfWork<DataContext>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


// builder.Services.AddDbContext<DataContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseEndpoints<Program>();

app.Run();