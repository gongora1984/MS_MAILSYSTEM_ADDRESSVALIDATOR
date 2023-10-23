using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;
using Microsoft.Extensions.Options;
using Quartz;

namespace MAILSYSTEM_ADDRESSVALIDATOR.BackgroundJobs;

public class ValidateAddressBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{
    private readonly QuartzOption _quartzOptions;

    public ValidateAddressBackgroundJobSetup(IOptions<QuartzOption> quartzOptions)
    {
        _quartzOptions = quartzOptions.Value;
    }

    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(ValidateAddressBackgroundJob));
        options.AddJob<ValidateAddressBackgroundJob>(jobBuilder =>
        {
            jobBuilder.WithIdentity(jobKey);
        })
        .AddTrigger(trigger =>
        {
            trigger
            .ForJob(jobKey)
            .WithSimpleSchedule(schedule =>
            {
                schedule.WithIntervalInMinutes(_quartzOptions.ValidatorInterval).RepeatForever();
            });
        });
    }
}
