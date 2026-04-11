using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Client (Tenant) entity for multi-tenant support
    /// </summary>
    public class Client : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; } // Unique identifier for the client
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? TaxOfficeNo { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        
        // Profile/Showcase fields
        /// <summary>
        /// Logo URL veya dosya yolu
        /// </summary>
        public string? LogoUrl { get; set; }

        /// <summary>
        /// Banner görseli URL (opsiyonel)
        /// </summary>
        public string? BannerUrl { get; set; }

        /// <summary>
        /// Enlem (harita koordinatı)
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Boylam (harita koordinatı)
        /// </summary>
        public double? Longitude { get; set; }
        
        /// <summary>
        /// Web sitesi URL
        /// </summary>
        public string? WebsiteUrl { get; set; }
        
        /// <summary>
        /// Detaylı hakkımızda açıklaması
        /// </summary>
        public string? AboutUs { get; set; }
        
        /// <summary>
        /// Çalışma saatleri (JSON formatında: {"Monday": "09:00-18:00", "Tuesday": "09:00-18:00", ...})
        /// </summary>
        public string? WorkingHours { get; set; }
        
        /// <summary>
        /// Sundukları hizmetler listesi (JSON formatında: ["Bakım", "Onarım", "Yedek Parça", ...])
        /// </summary>
        public string? Services { get; set; }
        
        /// <summary>
        /// Sosyal medya linkleri (JSON formatında: {"Facebook": "url", "Instagram": "url", ...})
        /// </summary>
        public string? SocialMediaLinks { get; set; }
        
        /// <summary>
        /// Profil public'te gösterilsin mi?
        /// </summary>
        public bool IsPublicProfileEnabled { get; set; } = false;
        
        // Navigation properties
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<ServicePortfolio> Portfolios { get; set; } = new List<ServicePortfolio>();
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public virtual ICollection<FacilityPhoto> FacilityPhotos { get; set; } = new List<FacilityPhoto>();
        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}

