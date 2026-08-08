namespace MagicCarRepairAISupported.Application.Common.Services.Web
{
    public interface IHtmlTemplateRenderer
    {
        /// <param name="email">Davet bağlantısındaki e-posta (form + API için).</param>
        /// <param name="token">Şifre sıfırlama token’ı (form + API için).</param>
        string RenderSetPasswordRedirect(string appLink, string email, string token);
    }
}

