using Vonage;
using Vonage.Messages.Sms;
using Vonage.Request;

var credentials = Credentials.FromAppIdAndPrivateKeyPath(
    "YOUR_APPLICATION_ID",
    "path/to/private.key");

var client = new VonageClient(credentials);

var response = await client.MessagesClient.SendAsync(new SmsRequest
{
    To = "YOUR_CELLPHONE_NUMBER",
    From = "YOUR_VONAGE_NUMBER",
    Text = "Testing status webhooks"
});

Console.WriteLine($"Message UUID: {response.MessageUuid}");
