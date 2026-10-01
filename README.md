# Receive SMS Delivery Receipts with ASP.NET Core MVC and the Messages API

This repository accompanies the blog post [How to Receive SMS Delivery Receipts with ASP.NET Core MVC](https://developer.vonage.com/en/blog). It contains an ASP.NET Core MVC app on .NET 10 that receives status callbacks (submitted, delivered, rejected) from the [Vonage Messages API](https://developer.vonage.com/en/messages/overview) and prints them to the console, plus a small console app for sending a test SMS.

* [Projects](#projects)
* [Requirements](#requirements)
* [Setup](#setup)
  * [Run the Webhook App](#run-the-webhook-app)
  * [Start ngrok](#start-ngrok)
  * [Create a Vonage Application and Configure Your Webhooks](#create-a-vonage-application-and-configure-your-webhooks)
  * [Send a Test Message](#send-a-test-message)
* [Contributing](#contributing)
* [License](#license)

## Projects

| Project | Description |
|---|---|
| `ReceiveMessageStatus` | ASP.NET Core MVC app exposing a `POST /webhooks/status` endpoint that binds the Messages API status callback to a model and logs it |
| `SendSmsMessage` | Console app that sends an SMS via the Messages API so you can watch the callbacks arrive |

## Requirements

* The [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
* A Vonage API account — [sign up here](https://dashboard.vonage.com/sign-up) if you don't have one
* The [Vonage CLI](https://developer.vonage.com/en/tools/vonage-cli) (`npm install -g @vonage/cli`)
* [ngrok](https://developer.vonage.com/en/getting-started/tools/ngrok) to expose your local app to the public internet

## Setup

### Run the Webhook App

```
cd ReceiveMessageStatus
dotnet run --urls "http://localhost:5000"
```

### Start ngrok

In a second terminal:

```
ngrok http http://localhost:5000
```

Note the forwarding address (e.g. `https://random-subdomain.ngrok-free.app`).

### Create a Vonage Application and Configure Your Webhooks

Create an application with the Messages capability, pointing the Status URL at your ngrok address (replace the subdomain with your own):

```
vonage apps create "Receive Message Status" \
  --messages_inbound_url=https://random-subdomain.ngrok-free.app/webhooks/inbound \
  --messages_status_url=https://random-subdomain.ngrok-free.app/webhooks/status
```

Note the Application ID this prints, and keep the `private.key` file it saves. Then link your Vonage virtual number to the application:

```
vonage numbers update VONAGE_NUMBER --app_id=YOUR_APPLICATION_ID
```

Finally, check that your account's Default SMS Setting (under [dashboard settings](https://dashboard.vonage.com/settings)) is set to **Messages API**.

### Send a Test Message

Open `SendSmsMessage/Program.cs` and replace `YOUR_APPLICATION_ID`, the private key path, and the `To`/`From` phone numbers with your own values (numbers in E.164 format without a leading `+` or `00`, e.g. `447700900000`). Then:

```
cd SendSmsMessage
dotnet run
```

In the console where `ReceiveMessageStatus` is running you'll see the callbacks arrive:

```
Message aaaaaaaa-bbbb-cccc-dddd-0123456789ab to 447700900000: submitted
Message aaaaaaaa-bbbb-cccc-dddd-0123456789ab to 447700900000: delivered
```

As always, make sure not to commit your sensitive credential data (including `private.key`) to any public version control — the included `.gitignore` covers it.

## Contributing

We ❤️ contributions from everyone! If you see something that needs fixed, then please follow the [GitHub Flow](https://guides.github.com/introduction/flow/index.html) and we'll try to incorporate it.

## License

This project is under the [MIT License](LICENSE).
