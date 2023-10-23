using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;
using Microsoft.Extensions.Options;

namespace MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup;

public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOption>
{
    private const string SectionName = "DatabaseConfig";
    private readonly IConfiguration _configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOption options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
