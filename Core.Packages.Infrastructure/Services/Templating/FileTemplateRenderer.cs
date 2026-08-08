using System.Text.Json;
using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Web;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services.Templating
{
    public class FileTemplateRenderer : IEmailTemplateRenderer, IHtmlTemplateRenderer
    {
        private readonly ILogger<FileTemplateRenderer> _logger;

        public FileTemplateRenderer(ILogger<FileTemplateRenderer> logger)
        {
            _logger = logger;
        }

        public string RenderEmployeeInvite(string firstName, string httpsLink, string appLink)
        {
            var template = ReadTemplateOrFallback("employee-invite.html", GetEmployeeInviteFallback());
            return Fill(template, new Dictionary<string, string>
            {
                ["firstName"] = firstName,
                ["httpsLink"] = httpsLink,
                ["appLink"] = appLink
            });
        }

        public string RenderSetPasswordRedirect(string appLink, string email, string token)
        {
            var template = ReadTemplateOrFallback("set-password-redirect.html", GetSetPasswordRedirectFallback());
            var intentLink = BuildAndroidIntentOpenUrl(appLink);
            var emailDisplay = System.Net.WebUtility.HtmlEncode(email);
            var emailAttr = System.Net.WebUtility.HtmlEncode(email ?? string.Empty);
            var tokenAttr = System.Net.WebUtility.HtmlEncode(token ?? string.Empty);
            return Fill(template, new Dictionary<string, string>
            {
                ["appLink"] = appLink,
                ["intentLink"] = intentLink,
                ["appLinkJs"] = JsonSerializer.Serialize(appLink),
                ["emailDisplay"] = emailDisplay,
                ["emailAttr"] = emailAttr,
                ["tokenAttr"] = tokenAttr,
            });
        }

        /// <summary>
        /// Chrome sometimes blocks programmatic navigation to custom schemes from http(s).
        /// A real anchor tap usually works; this intent URL is a fallback (no fixed package, no Play Store hop).
        /// </summary>
        private static string BuildAndroidIntentOpenUrl(string appLink)
        {
            const string prefix = "magiccarrepair://";
            if (string.IsNullOrWhiteSpace(appLink) || !appLink.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return appLink;

            var pathAndQuery = appLink[prefix.Length..];
            return "intent://" + pathAndQuery +
                   "#Intent;scheme=magiccarrepair;action=android.intent.action.VIEW;category=android.intent.category.BROWSABLE;end";
        }

        private string ReadTemplateOrFallback(string fileName, string fallback)
        {
            try
            {
                var baseDir = AppContext.BaseDirectory;
                var path = Path.Combine(baseDir, "Templates", fileName);
                if (!File.Exists(path))
                {
                    _logger.LogWarning("Template file not found at {Path}. Using fallback template.", path);
                    return fallback;
                }

                return File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read template {FileName}. Using fallback template.", fileName);
                return fallback;
            }
        }

        private static string Fill(string template, Dictionary<string, string> vars)
        {
            var result = template;
            foreach (var (key, value) in vars)
            {
                result = result.Replace("{{" + key + "}}", value ?? string.Empty, StringComparison.Ordinal);
            }
            return result;
        }

        /// <summary>Şablon dosyası diskte yoksa (deploy hatası) aynı HTML yapısının gömülü kopyası.</summary>
        private static string GetEmployeeInviteFallback() =>
            """
            <!DOCTYPE html>
            <html lang="tr">
            <head><meta charset="utf-8" /><meta name="viewport" content="width=device-width, initial-scale=1.0" /><title>Hesabınızı aktifleştirin — Magic Car Repair</title></head>
            <body style="margin:0;padding:0;background-color:#e8edf3;-webkit-text-size-adjust:100%;">
            <div style="display:none;max-height:0;overflow:hidden;font-size:1px;line-height:1px;color:#e8edf3;opacity:0;">Şifrenizi belirleyerek hesabınızı aktifleştirin — birkaç dakika sürer.</div>
            <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="background-color:#e8edf3;"><tr><td align="center" style="padding:24px 12px 40px 12px;">
            <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="max-width:600px;margin:0 auto;"><tr><td style="padding:0 0 16px 0;text-align:center;">
            <table role="presentation" cellspacing="0" cellpadding="0" border="0" align="center"><tr><td style="background-color:#1e3a8a;border-radius:12px;padding:14px 22px;">
            <span style="font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Arial,sans-serif;font-size:18px;font-weight:700;color:#ffffff;">Magic Car Repair</span>
            </td></tr></table></td></tr>
            <tr><td style="background-color:#ffffff;border-radius:16px;border:1px solid #e2e8f0;">
            <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0"><tr><td style="height:4px;background-color:#3c83f6;font-size:0;line-height:0;">&nbsp;</td></tr>
            <tr><td style="padding:32px 28px 8px 28px;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Arial,sans-serif;">
            <h1 style="margin:0 0 12px 0;font-size:24px;line-height:1.25;font-weight:700;color:#0f172a;">Merhaba {{firstName}},</h1>
            <p style="margin:0 0 20px 0;font-size:16px;line-height:1.6;color:#475569;">Ekibimize hoş geldiniz. Giriş yapabilmek için önce hesabınızı aktifleştirip <strong style="color:#0f172a;">şifrenizi belirlemeniz</strong> gerekiyor.</p>
            <p style="margin:0 0 28px 0;font-size:15px;line-height:1.55;color:#64748b;">Aşağıdaki güvenli bağlantıya dokunun. Tarayıcı açılacak ve uygulamanıza yönlendirileceksiniz.</p>
            </td></tr>
            <tr><td align="center" style="padding:0 28px 28px 28px;">
            <table role="presentation" cellspacing="0" cellpadding="0" border="0" align="center"><tr><td align="center" bgcolor="#3c83f6" style="border-radius:10px;">
            <a href="{{httpsLink}}" target="_blank" style="display:inline-block;padding:16px 36px;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Arial,sans-serif;font-size:16px;font-weight:600;color:#ffffff;text-decoration:none;border-radius:10px;">Şifremi belirle</a>
            </td></tr></table></td></tr>
            <tr><td style="padding:0 28px 28px 28px;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Arial,sans-serif;">
            <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="background-color:#f1f5f9;border-radius:12px;border:1px solid #e2e8f0;"><tr><td style="padding:16px 18px;">
            <p style="margin:0 0 8px 0;font-size:12px;font-weight:600;color:#64748b;text-transform:uppercase;letter-spacing:0.06em;">Sorun mu yaşıyorsunuz?</p>
            <p style="margin:0 0 10px 0;font-size:14px;line-height:1.55;color:#475569;">Buton çalışmıyorsa aşağıdaki metni kopyalayıp tarayıcıya yapıştırın.</p>
            <p style="margin:0;font-size:12px;word-break:break-all;font-family:Consolas,monospace;color:#334155;background:#fff;padding:12px 14px;border-radius:8px;border:1px solid #e2e8f0;">{{appLink}}</p>
            </td></tr></table></td></tr>
            <tr><td style="padding:0 28px 28px 28px;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Arial,sans-serif;">
            <p style="margin:0;font-size:13px;line-height:1.55;color:#94a3b8;">Bu davet bağlantısı <strong style="color:#64748b;">24 saat</strong> geçerlidir. Bu e-postayı siz talep etmediyseniz güvenle yok sayabilirsiniz.</p>
            </td></tr></table></td></tr>
            <tr><td style="padding:24px 8px 0 8px;text-align:center;font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Arial,sans-serif;font-size:12px;color:#94a3b8;">
            <p style="margin:0 0 6px 0;">© Magic Car Repair</p><p style="margin:0;">Bu mesaj otomatik olarak gönderilmiştir.</p>
            </td></tr></table></td></tr></table>
            </body></html>
            """;

        private static string GetSetPasswordRedirectFallback() =>
            """
            <!doctype html><html lang="tr"><head><meta charset="utf-8"/><meta name="viewport" content="width=device-width,initial-scale=1"/><title>Uygulamayı aç</title>
            <meta http-equiv="refresh" content="0;url={{appLink}}"/></head>
            <body style="margin:0;padding:20px;background:#0f172a;color:#e2e8f0;font-family:system-ui,sans-serif">
            <div style="max-width:440px;margin:0 auto 16px;padding:22px;text-align:center;border:1px solid rgba(255,255,255,.14);border-radius:16px;background:rgba(30,58,138,.4)">
            <h1 style="margin:0 0 8px;font-size:1.2rem">Uygulama</h1>
            <p style="margin:0 0 14px;font-size:.88rem;opacity:.88">Otomatik yönlendirme deneniyor. Olmazsa turuncu butona dokunun.</p>
            <a href="{{appLink}}" style="display:inline-block;width:100%;max-width:340px;padding:14px;border-radius:12px;background:#ff6d33;color:#fff;font-weight:800;text-decoration:none">Uygulamayı aç</a>
            <div style="margin-top:10px"><a href="{{intentLink}}" style="color:#93c5fd;font-size:.85rem">Android intent</a></div></div>
            <div style="max-width:440px;margin:0 auto;padding:16px;border:1px solid rgba(255,255,255,.1);border-radius:14px">
            <details><summary style="cursor:pointer;color:#93c5fd;font-weight:700">Tarayıcıda şifre</summary>
            <p style="font-size:.8rem;opacity:.8;margin:10px 0">8+ karakter, büyük/küçük/rakam/özel.</p>
            <p><b>Hesap:</b> {{emailDisplay}}</p>
            <input type="hidden" id="mcr-email" value="{{emailAttr}}"/><input type="hidden" id="mcr-token" value="{{tokenAttr}}"/>
            <form id="pwForm"><input id="pw1" type="password" minlength="8" required placeholder="Şifre" style="width:100%;margin:6px 0;padding:8px"/><input id="pw2" type="password" minlength="8" required placeholder="Tekrar" style="width:100%;margin:6px 0;padding:8px"/>
            <button type="submit" id="btn" style="width:100%;margin-top:8px;padding:10px;border:0;border-radius:10px;background:#3c83f6;color:#fff;font-weight:700">Kaydet</button></form>
            <div id="msg" style="margin-top:8px"></div></details></div>
            <script>try{window.location.replace({{appLinkJs}});}catch(e){}try{window.location.href={{appLinkJs}};}catch(e2){}</script>
            <script>(function(){var f=document.getElementById("pwForm"),m=document.getElementById("msg"),b=document.getElementById("btn"),e=document.getElementById("mcr-email"),t=document.getElementById("mcr-token");
            if(!f||!e||!t)return;var em=(e.value||"").trim(),tk=(t.value||"").trim();if(!em||!tk)return;
            f.onsubmit=async function(ev){ev.preventDefault();var p1=document.getElementById("pw1").value,p2=document.getElementById("pw2").value;if(p1.length<8){m.textContent="Min 8.";return;}if(p1!==p2){m.textContent="Eşleşmiyor.";return;}
            b.disabled=true;try{var r=await fetch("/api/Auth/set-employee-password",{method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify({email:em,token:tk,newPassword:p1,confirmPassword:p2})});
            var j=await r.json().catch(function(){return{}});var ok=j.success===true||j.Success===true,msg=j.message||j.Message||"";
            if(ok){m.style.color="#86efac";m.textContent=msg||"OK";f.style.display="none";}else{m.style.color="#fca5a5";m.textContent=msg||"Hata";}}catch(x){m.textContent="Ağ";}finally{b.disabled=false;}};})();</script>
            </body></html>
            """;
    }
}

