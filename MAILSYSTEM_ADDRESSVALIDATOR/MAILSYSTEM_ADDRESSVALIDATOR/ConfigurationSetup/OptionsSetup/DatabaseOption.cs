namespace MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;

public class DatabaseOption
{
    public string DbHost { get; init; } = string.Empty;
    public string DbName { get; init; } = string.Empty;
    public string DbUser { get; init; } = string.Empty;
    public string DbPassword { get; init; } = string.Empty;
    public int DbPort { get; init; }
    public int DbMaxRetryCount { get; init; }
    public int DbCommandTimeOut { get; init; }
    public bool DbEnableDetailedError { get; init; }
    public bool DbEnableSensitiveDataLogging { get; init; }
    public bool DbTrustServerCertificate { get; init; }
    public bool DbMultiSubnetFailover { get; init; }
    public bool DbMultipleActiveResultSets { get; init; }
}
