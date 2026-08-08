using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Parça entity'si
    /// </summary>
    public class Part : BaseEntity<int>, IClientEntity
    {
        public string PartCode { get; set; } // Parça kodu (benzersiz)
        public string Name { get; set; }
        public string? Description { get; set; }
        public PartCategory Category { get; set; }
        public PartBrandType BrandType { get; set; } // Orijinal/Emsal
        public string? Brand { get; set; } // Marka (örn: "Bosch", "Mann Filter")
        public string? OEMNumber { get; set; } // Orijinal ekipman numarası
        public string? Barcode { get; set; }
        
        // Fiyat bilgileri
        public decimal PurchasePrice { get; set; } // Alış fiyatı
        public decimal SalePrice { get; set; } // Satış fiyatı
        public decimal TaxRate { get; set; } = 20; // KDV oranı (varsayılan %20)
        
        // Stok bilgileri
        public int MinimumStockLevel { get; set; } = 0; // Minimum stok seviyesi
        public bool IsLowStockAlertEnabled { get; set; } = true; // Kritik stok uyarısı aktif mi?
        
        // Tedarikçi bilgisi
        public int? SupplierId { get; set; }
        public virtual PartSupplier? Supplier { get; set; }
        
        // Birim bilgisi
        public string Unit { get; set; } = "Adet"; // Birim (Adet, Litre, Kg, vb.)
        
        // Garanti bilgisi
        public int? WarrantyMonths { get; set; } // Garanti süresi (ay)
        
        public string? Notes { get; set; }

        // ── Araç uyumluluk alanları ──────────────────────────────────────
        /// <summary>
        /// Uyumlu araç markaları — JSON dizi: ["BMW","MINI"]
        /// Boşsa tüm markalara uyumlu kabul edilir.
        /// </summary>
        public string? CompatibleVehicleBrands { get; set; }

        /// <summary>
        /// Uyumlu araç modelleri — JSON dizi: ["3 Serisi","5 Serisi"]
        /// Boşsa CompatibleVehicleBrands içindeki tüm modellere uyumlu.
        /// </summary>
        public string? CompatibleVehicleModels { get; set; }

        /// <summary>En düşük uyumlu model yılı (dahil). Null = kısıtlama yok.</summary>
        public int? CompatibleYearFrom { get; set; }

        /// <summary>En yüksek uyumlu model yılı (dahil). Null = kısıtlama yok.</summary>
        public int? CompatibleYearTo { get; set; }

        /// <summary>
        /// Ek OEM/parça kodları — JSON dizi: ["51647393613","51647220285"]
        /// Birden fazla OEM kodu desteklemek için (OEMNumber tek değer için kalır).
        /// </summary>
        public string? AdditionalOemCodes { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        
        // Navigation properties
        public virtual PartStock? Stock { get; set; }
        public virtual ICollection<StockMovement> StockMovements { get; set; }
        public virtual ICollection<PartPhoto> Photos { get; set; } = new List<PartPhoto>();
        
        /// <summary>
        /// Kritik stok seviyesinde mi kontrol eder
        /// </summary>
        public bool IsLowStock()
        {
            return Stock != null && Stock.Quantity <= MinimumStockLevel && IsLowStockAlertEnabled;
        }
    }
}

