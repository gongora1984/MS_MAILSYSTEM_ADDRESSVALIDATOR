using MAILSYSTEM_ADDRESSVALIDATOR.DependencyInjection;
using MAILSYSTEM_ADDRESSVALIDATOR.DependencyInjection.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .InstallServices(
        builder.Configuration,
        typeof(IServiceInstaller).Assembly);

var app = builder.Build();


app.Run();

