using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Bildirim şablonu entity'si
    /// </summary>
    public class NotificationTemplate : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Şablon adı (örn: "QuoteRequestCreated", "WorkOrderCompleted")
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Şablon açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Bildirim tipi
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Şablon başlığı (değişkenler içerebilir: {RequestNumber}, {CustomerName}, vb.)
        /// </summary>
        public string TitleTemplate { get; set; } = string.Empty;

        /// <summary>
        /// Şablon içeriği (değişkenler içerebilir)
        /// </summary>
        public string ContentTemplate { get; set; } = string.Empty;

        /// <summary>
        /// İlgili Entity Tipi
        /// </summary>
        public string? RelatedEntityType { get; set; }

        /// <summary>
        /// Aktif mi?
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Şablonu değişkenlerle doldurur
        /// </summary>
        public string FillTemplate(string template, Dictionary<string, object> variables)
        {
            var result = template;
            foreach (var variable in variables)
            {
                result = result.Replace($"{{{variable.Key}}}", variable.Value?.ToString() ?? string.Empty);
            }
            return result;
        }

        /// <summary>
        /// Başlık şablonunu doldurur
        /// </summary>
        public string GetFilledTitle(Dictionary<string, object> variables)
        {
            return FillTemplate(TitleTemplate, variables);
        }

        /// <summary>
        /// İçerik şablonunu doldurur
        /// </summary>
        public string GetFilledContent(Dictionary<string, object> variables)
        {
            return FillTemplate(ContentTemplate, variables);
        }
    }
}

