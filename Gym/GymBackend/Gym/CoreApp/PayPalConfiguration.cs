// CoreApp/PayPal/PayPalConfiguration.cs

using PayPal.Api;

namespace CoreApp.PayPal;

public static class PayPalConfiguration
{
    public static readonly string ClientId =
        "ATfC0-7M60YuIl8XwbW5A45MDs7GBBFaAg3uJj4GqCTR4eNV8XxCXpS30Qu-RhSBo7PZsX_f4PlFopZ-";

    public static readonly string ClientSecret =
        "EFyRCq7-G9GAA2TCEO8Fu2B0wcsyA2e_9EOxbVZZL2KAox2WtxxVu_tJxD6PiUsOY3BUbInUgyWm0MVj";

    public static APIContext GetAPIContext()
    {
        var config = GetConfig();
        var accessToken = new OAuthTokenCredential(ClientId, ClientSecret, config).GetAccessToken();
        return new APIContext(accessToken)
        {
            Config = config
        };
    }

    private static Dictionary<string, string> GetConfig()
    {
        return new Dictionary<string, string>
        {
            { "mode", "sandbox" }, // Cambia a "live" para producción
            { "clientId", ClientId },
            { "clientSecret", ClientSecret }
        };
    }
}