namespace MAILSYSTEM_ADDRESSVALIDATOR.DependencyInjection.Interfaces;

public interface IServiceInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
}
