using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Tesis fotoğrafı entity'si - Tamirhane tesislerinin fotoğrafları
    /// </summary>
    public class FacilityPhoto : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Fotoğraf yolu
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// Başlık
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Kategori (Dış görünüm, İç mekan, Atölye, Bekleme salonu, vb.)
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Public profilde gösterilsin mi?
        /// </summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>
        /// Sıralama
        /// </summary>
        public int DisplayOrder { get; set; } = 0;

        /// <summary>
        /// Upload tarihi
        /// </summary>
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
