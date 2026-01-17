using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Commands.Send
{
    public class SendNotificationCommand : IRequest<IResult>
    {
        /// <summary>
        /// Bildirim tipi
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Alıcı User ID (opsiyonel)
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Alıcı Email (Email için)
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Alıcı Telefon (SMS için)
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Başlık
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// İçerik
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// İlgili Entity Tipi
        /// </summary>
        public string? RelatedEntityType { get; set; }

        /// <summary>
        /// İlgili Entity ID
        /// </summary>
        public int? RelatedEntityId { get; set; }
    }
}

