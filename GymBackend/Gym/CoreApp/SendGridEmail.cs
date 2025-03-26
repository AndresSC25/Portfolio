using SendGrid;
using SendGrid.Helpers.Mail;

public class SendGridEmail
{
    private readonly string _apiKey;

    public SendGridEmail()
    {
        _apiKey = "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";
    }

    //Se pueden crear mas templates del email :)
    public void SendEmailAsync(string toEmail, string passwordR)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("siluetaclubfitness@gmail.com", "Silueta Club Fitness");
        var to = new EmailAddress(toEmail);

        var templateId = "d-e87f1f3d83064e929f7dcdb90faf8352";
        var dynamicTemplateData = new
        {
            subject = "Clave Temporal de Silueta Club Fitness",
            password = passwordR
        };
        var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
        var response = client.SendEmailAsync(msg);
    }

    //Se quita el async y el await por problemas con el codigo.
    public void SendEmailAsyncPasswordChanges(string toEmail)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("siluetaclubfitness@gmail.com", "Silueta Club Fitness");
        var to = new EmailAddress(toEmail);

        var templateId = "d-68185f3204154cee9b778c59f00da17d";
        var dynamicTemplateData = new
        {
            subject = "Su clave ha sido cambiada"
        };
        var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);
        var response = client.SendEmailAsync(msg);
    }
}