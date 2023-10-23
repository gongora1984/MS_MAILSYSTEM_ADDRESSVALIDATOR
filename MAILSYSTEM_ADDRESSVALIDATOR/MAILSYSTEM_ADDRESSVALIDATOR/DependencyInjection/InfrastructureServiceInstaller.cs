using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;
using MAILSYSTEM_ADDRESSVALIDATOR.DependencyInjection.Interfaces;
using MAILSYSTEM_ADDRESSVALIDATOR.Infrastructure;
using MAILSYSTEM_ADDRESSVALIDATOR.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MAILSYSTEM_ADDRESSVALIDATOR.DependencyInjection;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            (sp, optionsBuilder) =>
            {
                var databaseOptions = sp.GetService<IOptions<DatabaseOption>>()!.Value;
                ////string dbConnectionString = configuration.BuildDbConnectionString();
                string dbConnectionString = DbConfigurationExtensions.BuildDbConnectionString(databaseOptions);
                optionsBuilder.UseSqlServer(
                    dbConnectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    .EnableRetryOnFailure(databaseOptions.DbMaxRetryCount)
                    .CommandTimeout(databaseOptions.DbCommandTimeOut));

                optionsBuilder.EnableDetailedErrors(databaseOptions.DbEnableDetailedError);
                optionsBuilder.EnableSensitiveDataLogging(databaseOptions.DbEnableSensitiveDataLogging);
            });
    }
}
