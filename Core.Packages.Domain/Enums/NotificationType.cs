using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Domain.Enums
{
    public enum NotificationType
    {
        Email,
        Sms,
        Push,
        /// <summary>WhatsApp Business API ile gönderim (kayıt / raporlama için)</summary>
        WhatsApp
    }
}
