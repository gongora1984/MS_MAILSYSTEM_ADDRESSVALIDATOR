namespace MAILSYSTEM_ADDRESSVALIDATOR.Services;

public interface IStampsServiceProvider
{
    srvStamp.Address ClensAddress(srvStamp.Address ad, out bool hasMatch);
}
