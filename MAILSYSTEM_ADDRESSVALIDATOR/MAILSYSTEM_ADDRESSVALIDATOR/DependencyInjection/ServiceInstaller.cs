using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup;
using MAILSYSTEM_ADDRESSVALIDATOR.DependencyInjection.Interfaces;
using MAILSYSTEM_ADDRESSVALIDATOR.Services;

namespace MAILSYSTEM_ADDRESSVALIDATOR.DependencyInjection;

public class ServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<StampsOptionsSetup>();
        services.ConfigureOptions<DatabaseOptionsSetup>();

        services.AddScoped<IStampsServiceProvider, StampsServiceProvider>();


    }
}
