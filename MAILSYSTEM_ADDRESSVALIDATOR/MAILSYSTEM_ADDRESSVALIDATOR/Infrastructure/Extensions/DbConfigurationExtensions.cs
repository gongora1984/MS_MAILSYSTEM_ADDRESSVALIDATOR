using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Infrastructure.Extensions;

public static class DbConfigurationExtensions
{
    public static string BuildDbConnectionString(DatabaseOption dbOptions)
    {
        ////var builder = new SqlConnectionStringBuilder
        ////{
        ////    DataSource = string.Format("{0},{1}", dbOptions.DbHost, dbOptions.DbPort),
        ////    InitialCatalog = dbOptions.DbName,
        ////    UserID = dbOptions.DbUser,
        ////    Password = dbOptions.DbPassword,
        ////    ConnectTimeout = dbOptions.DbCommandTimeOut,
        ////    TrustServerCertificate = dbOptions.DbTrustServerCertificate,
        ////    MultiSubnetFailover = dbOptions.DbMultiSubnetFailover,
        ////    MultipleActiveResultSets = dbOptions.DbMultipleActiveResultSets
        ////};
        ////return builder.ConnectionString;

        var connectionstring = $"" +
            $"Server={dbOptions.DbHost};" +
            $"Initial Catalog={dbOptions.DbName};" +
            $"User ID={dbOptions.DbUser};" +
            $"Password={dbOptions.DbPassword};" +
            $"MultipleActiveResultSets={dbOptions.DbMultipleActiveResultSets};" +
            $"TrustServerCertificate={dbOptions.DbTrustServerCertificate};" +
            $"MultiSubnetFailover={dbOptions.DbMultiSubnetFailover}";

        return connectionstring;
    }
}
