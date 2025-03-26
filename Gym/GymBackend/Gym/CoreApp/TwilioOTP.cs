using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CoreApp;

public class TwilioOTP
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromPhoneNumber;

    public TwilioOTP()
    {
        _accountSid = "AC8edd12ff12c93591c60ed263148837e5";
        _authToken = "cb61418e6f04c7d99f632fd24931370a";
        _fromPhoneNumber = "+12183054203";

        TwilioClient.Init(_accountSid, _authToken);
    }

    public void SendMessage(string toPhoneNumber, int otp)
    {
        var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
        {
            From = new PhoneNumber(_fromPhoneNumber),
            Body = "Este es tu código de autentificación enviado por Silueta Fitness Club: " + otp
        };

        var msg = MessageResource.Create(messageOptions);
    }


    public static string GenerateOTP()
    {
        var random = new Random();
        return random.Next(100000, 1000000).ToString();
    }
}