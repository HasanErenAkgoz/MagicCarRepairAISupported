namespace MagicCarRepairAISupported.Application.Common.Services.Email
{
    public interface IEmailTemplateRenderer
    {
        string RenderEmployeeInvite(string firstName, string httpsLink, string appLink);
    }
}

