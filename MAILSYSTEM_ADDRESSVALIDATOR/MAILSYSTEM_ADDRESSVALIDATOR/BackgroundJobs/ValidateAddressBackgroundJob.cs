using MAILSYSTEM_ADDRESSVALIDATOR.Domain;
using MAILSYSTEM_ADDRESSVALIDATOR.Infrastructure;
using MAILSYSTEM_ADDRESSVALIDATOR.Services;
using Quartz;
using srvStamp;

namespace MAILSYSTEM_ADDRESSVALIDATOR.BackgroundJobs;

[DisallowConcurrentExecution]
public class ValidateAddressBackgroundJob : IJob
{
    private readonly ILogger<ValidateAddressBackgroundJob> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IStampsServiceProvider _svcStamp;

    public ValidateAddressBackgroundJob(
        ILogger<ValidateAddressBackgroundJob> logger,
        ApplicationDbContext dbContext,
        IStampsServiceProvider svcStamp)
    {
        _logger = logger;
        _dbContext = dbContext;
        _svcStamp = svcStamp;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var pendingJobs = _dbContext.Set<MailJob>().Where(x => x.VerifiedOn == null && (x.Voided == false || x.NeedManualCorrection == false)).ToList();

        if (!pendingJobs.Any())
        {
            _logger.LogInformation("No pending jobs waiting for verification.");
            return Task.CompletedTask;
        }

        foreach (var job in pendingJobs)
        {
            var jobDetails = _dbContext.Set<MailJobDetail>().Where(x => x.MailJobId == job.Id);

            if (!jobDetails.Any())
            {
                job.Voided = true;
                job.VoidedOn = DateTime.Now;
                job.VoidedNotes = @"Mising job details";

                _dbContext.SaveChanges();

                continue;
            }

            var jobDetailsCount = jobDetails.Count();

            foreach (var details in jobDetails)
            {
                var wasProccesed = ProcessSingleJob(job, details);

                if (wasProccesed)
                    jobDetailsCount--;

            }

            if (jobDetailsCount == 0)
            {
                job.VerifiedOn = DateTime.Now;

                _dbContext.SaveChanges();
            }
            else
            {
                job.NeedManualCorrection = true;

                _dbContext.SaveChanges();
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Process Single Job
    /// </summary>
    /// <param name="job"></param>
    /// <param name="details"></param>
    private bool ProcessSingleJob(MailJob job, MailJobDetail details)
    {
        var wasProcessed = true;
        var address = new Address
        {
            Address1 = details.RecipientAddress1,
            Address2 = details.RecipientAddress2,
            Address3 = details.RecipientAddress3,
            City = details.RecipientCity,
            State = GetStateAbbById(Guid.Parse(details.RecipientState.ToString())),
            ZIPCode = details.RecipientZip,
            PostalCode = details.RecipientZip,
            FullName = details.RecipientName
        };

        var finalAddressTo = _svcStamp.ClensAddress(address, out bool hasMatch);

        if (hasMatch)
        {
            //need to correct the to address into the system 
            details.VerifiedOn = DateTime.Now;
            details.WasCorrected = true;
            details.CorrectedOn = DateTime.Now;

            details.ChangedRecipientName = finalAddressTo.FullName;
            details.ChangedRecipientAddress1 = finalAddressTo.Address1;
            details.ChangedRecipientAddress2 = finalAddressTo.Address2;
            details.ChangedRecipientAddress3 = finalAddressTo.Address3;
            details.ChangedRecipientCity = finalAddressTo.City;
            details.ChangedRecipientState = GetStateIdByAbb(finalAddressTo.State);
            details.ChangedRecipientZip = finalAddressTo.ZIPCode;

            _dbContext.SaveChanges();

            wasProcessed = true;
        }
        else
        {
            details.NeedCorrection = true;
            _dbContext.SaveChanges();
        }

        return wasProcessed;
    }

    private Guid GetStateIdByAbb(string stateAbb)
    {
        var stateInfo = _dbContext.Set<State>().FirstOrDefault(s => s.StateAbbreviation == stateAbb);

        if (stateInfo == null)
            return Guid.Empty;

        return stateInfo.Id;
    }

    private string GetStateAbbById(Guid stateId)
    {
        var stateInfo = _dbContext.Set<State>().FirstOrDefault(s => s.Id == stateId);

        if (stateInfo == null)
            return string.Empty;

        return stateInfo.StateAbbreviation;
    }
}
