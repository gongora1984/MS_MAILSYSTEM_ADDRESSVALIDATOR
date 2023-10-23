using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;
using Microsoft.Extensions.Options;

namespace MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup;

public class QuartzOptionSetup : IConfigureOptions<QuartzOption>
{
    private const string SectionName = "Quartz";
    private readonly IConfiguration _configuration;

    public QuartzOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(QuartzOption options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
