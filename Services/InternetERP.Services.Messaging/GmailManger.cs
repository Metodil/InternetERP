namespace InternetERP.Services.Messaging
{
    using System;

    using Google.Apis.Auth.OAuth2;
    using MailKit.Security;
    using MimeKit.Text;
    using MimeKit;
    using System.Threading;
    using System.Threading.Tasks;
    using MailKit.Net.Smtp;

    public class GmailManger : IGmailManager
    {

        private async Task<bool> SendMailAsync()
        {
            //string clientID = "XXX.apps.googleusercontent.com";
            //string clientSecret = "IxBs0g5sdaSDUz4Ea7Ix-Ua";
            //string redirectUri = "https://www.example.com/oauth2callback";

            //var clientSecrets = new ClientSecrets
            //{
            //    ClientId = clientID,
            //    ClientSecret = clientSecret
            //};

            //var credential = new GoogleAuthorizationCodeFlow(
            //new GoogleAuthorizationCodeFlow.Initializer
            //{
            //    ClientSecrets = clientSecrets,
            //    Scopes = new[] {
            //    GoogleScope.ImapAndSmtp.Name,
            //    GoogleScope.UserInfoEmailScope.Name}
            //});


            try
            {
                string clientId = "Your Client Id";
                string clientSecret = "Your Client Secret Key";
                string fromMail = "Your Register Email Id";
                string ToMail = "metodil@gmail.com";
                string[] scopes = new string[] { "https://mail.google.com/" };
                ClientSecrets clientSecrets = new()
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                };
                //Requesting authorization
                UserCredential userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, "user", CancellationToken.None).Result;
                //Authorization granted or not required (if the saved access token already available)
                if (userCredential.Token.IsExpired(userCredential.Flow.Clock))
                {
                    //The access token has expired, refreshing it
                    if (!userCredential.RefreshTokenAsync(CancellationToken.None).Result)
                    {
//                        return StatusCode(500);
                        return false;
                    }
                }
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(fromMail));
                email.To.Add(MailboxAddress.Parse(ToMail));
//                email.To.Add(MailboxAddress.Parse(emailId));
                email.Subject = "Sending Email Using OAuth Net 6.0";
                email.Body = new TextPart(TextFormat.Html) { Text = "<h3>Mail Body</h3>" };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    var oauth2 = new SaslMechanismOAuth2(fromMail, userCredential.Token.AccessToken);
                    client.Authenticate(oauth2);
                    await client.SendAsync(email);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
