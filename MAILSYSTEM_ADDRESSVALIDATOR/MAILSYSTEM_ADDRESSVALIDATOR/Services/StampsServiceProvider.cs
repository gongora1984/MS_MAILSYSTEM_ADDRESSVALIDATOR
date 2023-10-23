using MAILSYSTEM_ADDRESSVALIDATOR.ConfigurationSetup.OptionsSetup;
using System.Net;
using static svcStamp.SwsimV135SoapClient;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Services;

public class StampsServiceProvider
{
    private readonly svcStamp.SwsimV135SoapClient srvcSDC = new svcStamp.SwsimV135SoapClient(EndpointConfiguration.SwsimV135Soap);

    private readonly StampsOption _stampsOptions;


    public StampsServiceProvider(StampsOption stampsOptions)
    {
        _stampsOptions = stampsOptions;
    }

    /// <summary>
    /// SdcKey from Configuration file.
    /// </summary>
    public string SdcKey { get; set; }

    /// <summary>
    /// Clens Address - Address Validation.
    /// </summary>
    /// <param name="ad"></param>
    /// <param name="hasMatch"></param>
    /// <returns></returns>
    public async Task<CleanseAddressResults> ClensAddress(svcStamp.Address ad, string FromZip)
    {
        SdcKey = _stampsOptions.SdcKey;
        var hasMatch = false;
        var rtn = new svcStamp.Address()
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
            var cred = new svcStamp.Credentials
            {
                IntegrationID = Guid.Parse(SdcKey),
                Username = _stampsOptions.StampsUN,
                Password = _stampsOptions.StampsPW
            };
            //cred.Username = "unitedprint";//jvanbrocklin
            //cred.Password = "postage1";//FloridaSun8*
            bool addressmtch1;
            bool cityzipok1;
            svcStamp.ResidentialDeliveryIndicatorType restype1;
            svcStamp.RateV46[] rates1;
            svcStamp.RateV46[] rates2;
            bool? ispobox1;

            svcStamp.Address[] retaddresses;
            svcStamp.StatusCodes statcodes;
            var AdClensRest = "";
            var verifLevel = new svcStamp.AddressVerificationLevel();
            if (ad.ZIPCode.Length > 5)
            {
                ad.ZIPCode = ad.ZIPCode.Substring(0, 5);
                ad.PostalCode = ad.PostalCode.Substring(0, 5);
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                             | SecurityProtocolType.Tls11
                                             | SecurityProtocolType.Tls12;
            var cleanseResponse = await srvcSDC.CleanseAddressAsync(new svcStamp.CleanseAddressRequest
            {
                Item = cred,
                Address = ad,
                FromZIPCode = FromZip
            });
            //newsdcKey = await srvcSDC.CleanseAddressAsync(new svcStamp.CleanseAddressRequest
            //{

            //}
            //                    cred,
            //                    ref ad,
            //                    string.Empty,//ad.ZIPCode,
            //                    out addressmtch1,
            //                    out cityzipok1,
            //                    out restype1,
            //                    out ispobox1,
            //                    out retaddresses,
            //                    out statcodes,
            //                    out rates1,
            //                    out AdClensRest,
            //                    out verifLevel);
            if (cleanseResponse.AddressMatch)
            {
                hasMatch = true;
                rtn = ad;
            }
            else
            {
                if (cleanseResponse.CandidateAddresses?.Length > 0)
                {
                    rtn = cleanseResponse.CandidateAddresses[0];
                    hasMatch = true;
                }
            }
        }
        catch (Exception ex)
        { }

        return new CleanseAddressResults
        {
            FinalAddress = rtn,
            HasMatch = hasMatch
        };
    }

    public sealed class CleanseAddressResults
    {
        public svcStamp.Address? FinalAddress { get; set; }

        public bool HasMatch { get; set; }
    }
}
