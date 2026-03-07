using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Chat mesajı entity'si
    /// </summary>
    public class ChatMessage : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Gönderen kullanıcı ID
        /// </summary>
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }

        /// <summary>
        /// Alıcı kullanıcı ID (null ise grup mesajı)
        /// </summary>
        public int? ReceiverId { get; set; }
        public virtual User? Receiver { get; set; }

        /// <summary>
        /// Mesaj içeriği
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Mesaj tipi (Text, Image, File, System)
        /// </summary>
        public ChatMessageType MessageType { get; set; } = ChatMessageType.Text;

        /// <summary>
        /// İlgili WorkOrder ID (opsiyonel)
        /// </summary>
        public int? WorkOrderId { get; set; }
        public virtual WorkOrder? WorkOrder { get; set; }

        /// <summary>
        /// İlgili Customer ID (opsiyonel - müşteri-servis arası mesajlaşma)
        /// </summary>
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Dosya yolu (eğer dosya gönderildiyse)
        /// </summary>
        public string? FilePath { get; set; }

        /// <summary>
        /// Dosya adı
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Dosya boyutu (bytes)
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// Mesaj okundu mu?
        /// </summary>
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// Okunma tarihi
        /// </summary>
        public DateTime? ReadDate { get; set; }

        /// <summary>
        /// Mesaj gönderim tarihi
        /// </summary>
        public DateTime SentDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Mesajı okundu olarak işaretle
        /// </summary>
        public void MarkAsRead()
        {
            IsRead = true;
            ReadDate = DateTime.UtcNow;
        }
    }
}
