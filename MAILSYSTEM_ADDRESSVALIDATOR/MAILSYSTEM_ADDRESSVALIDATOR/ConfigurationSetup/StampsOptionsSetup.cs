using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;
using Microsoft.Extensions.Options;

namespace MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup;

public class StampsOptionsSetup : IConfigureOptions<StampsOption>
{
    private const string SectionName = "Stamps";
    private readonly IConfiguration _configuration;

    public StampsOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(StampsOption options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
