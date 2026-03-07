using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Yardım makalesi entity'si
    /// </summary>
    public class HelpArticle : BaseEntity<int>, IClientEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // Markdown formatında
        public string Category { get; set; } = string.Empty; // e.g., "Getting Started", "Work Orders", "Customers"
        public int Order { get; set; } = 0;
        public bool IsPublished { get; set; } = true;
        public int ViewCount { get; set; } = 0;
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
