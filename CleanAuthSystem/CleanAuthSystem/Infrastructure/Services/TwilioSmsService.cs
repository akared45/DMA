using CleanAuthSystem.Domain.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

public class TwilioSmsService : ISmsService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromPhone;

    public TwilioSmsService(string accountSid, string authToken, string fromPhone)
    {
        _accountSid = accountSid;
        _authToken = authToken;
        _fromPhone = fromPhone;

        TwilioClient.Init(_accountSid, _authToken);
    }

    public async Task SendSmsAsync(string to, string message)
    {
        await MessageResource.CreateAsync(
            to: new Twilio.Types.PhoneNumber(to),
            from: new Twilio.Types.PhoneNumber(_fromPhone),
            body: message
        );
    }
}
