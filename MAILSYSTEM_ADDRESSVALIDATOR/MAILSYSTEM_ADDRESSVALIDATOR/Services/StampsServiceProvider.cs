using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;
using Microsoft.Extensions.Options;
using System.Net;
using static srvStamp.SwsimV135SoapClient;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Services;

public class StampsServiceProvider : IStampsServiceProvider
{
    private readonly srvStamp.SwsimV135SoapClient srvcSDC = new srvStamp.SwsimV135SoapClient(EndpointConfiguration.SwsimV135Soap);

    private readonly StampsOption _stampsOptions;


    public StampsServiceProvider(IOptions<StampsOption> stampsOptions)
    {
        _stampsOptions = stampsOptions.Value;
    }

    /// <summary>
    /// SdcKey from Configuration file.
    /// </summary>
    public string SdcKey { get; set; } = string.Empty;

    /// <summary>
    /// Clens Address - Address Validation.
    /// </summary>
    /// <param name="ad"></param>
    /// <param name="hasMatch"></param>
    /// <returns></returns>
    public srvStamp.Address ClensAddress(srvStamp.Address ad, out bool hasMatch)
    {
        SdcKey = _stampsOptions.SdcKey;

        hasMatch = false;
        var rtn = new srvStamp.Address()
        {
            Address1 = ad.Address1,
            Address2 = ad.Address2,
            Address3 = ad.Address3,
            City = ad.City,
            State = ad.State,
            ZIPCode = ad.ZIPCode,
            FullName = ad.FullName,
            PostalCode = ad.PostalCode
        };
        try
        {
            var cred = new srvStamp.Credentials
            {
                IntegrationID = Guid.Parse(SdcKey),
                Username = _stampsOptions.StampsUN,
                Password = _stampsOptions.StampsPW
            };
            //cred.Username = "unitedprint";//jvanbrocklin
            //cred.Password = "postage1";//FloridaSun8*
            bool addressmtch1;
            bool cityzipok1;
            srvStamp.ResidentialDeliveryIndicatorType restype1;
            srvStamp.RateV46[] rates1;
            bool? ispobox1;

            srvStamp.Address[] retaddresses;
            srvStamp.StatusCodes statcodes;
            var newsdcKey = "";
            var AdClensRest = "";
            var verifLevel = new srvStamp.AddressVerificationLevel();
            if (ad.ZIPCode.Length > 5)
            {
                ad.ZIPCode = ad.ZIPCode.Substring(0, 5);
                ad.PostalCode = ad.PostalCode.Substring(0, 5);
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                             | SecurityProtocolType.Tls11
                                             | SecurityProtocolType.Tls12;
            newsdcKey = srvcSDC.CleanseAddress(
                                cred,
                                ref ad,
                                string.Empty,//ad.ZIPCode,
                                out addressmtch1,
                                out cityzipok1,
                                out restype1,
                                out ispobox1,
                                out retaddresses,
                                out statcodes,
                                out rates1,
                                out AdClensRest,
                                out verifLevel);
            if (addressmtch1)
            {
                hasMatch = true;
                rtn = ad;
            }
            else
            {
                if (retaddresses?.Length > 0)
                {
                    rtn = retaddresses[0];
                    hasMatch = true;
                }
            }
        }
        catch (Exception ex)
        { }

        return rtn;
    }

    public class CleanseAddressResults
    {
        public srvStamp.Address? FinalAddress { get; set; }

        public bool HasMatch { get; set; }
    }
}
