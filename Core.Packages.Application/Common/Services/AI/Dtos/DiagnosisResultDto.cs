namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    public enum DiagnosisType
    {
        Mechanical = 0,
        Accident = 1,
        Maintenance = 2
    }

    public enum DamageLevel
    {
        None = 0,
        Light = 1,
        Medium = 2,
        Heavy = 3,
        Critical = 4
    }

    public enum RepairAction
    {
        None = 0,
        Paint = 1,
        Repair = 2,
        Replace = 3
    }

    public enum RiskLevel
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Critical = 3
    }

    /// <summary>
    /// AI arıza tespiti sonuç DTO'su
    /// </summary>
    public class DiagnosisResultDto
    {
        /// <summary>
        /// Tespit edilen arıza türleri (olasılık skorları ile)
        /// </summary>
        public List<DiagnosisItemDto> PossibleIssues { get; set; } = new();

        /// <summary>
        /// Önerilen parçalar
        /// </summary>
        public List<RecommendedPartDto> RecommendedParts { get; set; } = new();

        /// <summary>
        /// Önerilen işçilikler
        /// </summary>
        public List<RecommendedLaborDto> RecommendedLabors { get; set; } = new();

        /// <summary>
        /// Tahmini süre (gün)
        /// </summary>
        public int? EstimatedDays { get; set; }

        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        /// Güven skoru (0-100)
        /// </summary>
        public int ConfidenceScore { get; set; }

        /// <summary>
        /// Öneriler
        /// </summary>
        public string? Recommendations { get; set; }

        /// <summary>
        /// Teşhis tipi (Mekanik, Kaza, Bakım)
        /// </summary>
        public DiagnosisType DiagnosisType { get; set; } = DiagnosisType.Mechanical;

        /// <summary>
        /// Hasarlı parçalar (kaza analizi)
        /// </summary>
        public List<DamagedPartDto> DamagedParts { get; set; } = new();

        /// <summary>
        /// Kritik kontroller (kaza analizi)
        /// </summary>
        public List<CriticalCheckDto> CriticalChecks { get; set; } = new();

        /// <summary>
        /// Tahmini onarım maliyet aralığı (kaza analizi)
        /// </summary>
        public EstimatedRepairRangeDto? EstimatedRepairRange { get; set; }

        /// <summary>
        /// Mobil uygulamada Markdown olarak gösterilecek birleşik özet (kaza/hasar modunda).
        /// Başlıklar, emoji ve madde işaretleri ile chat benzeri okuma deneyimi için.
        /// </summary>
        public string? MobileDisplayMarkdown { get; set; }

        /// <summary>
        /// Fotoğraf(lar) için: kamera açısı, kadrajda görünen araç bölgeleri ve görünmeyen bölgeler (kaza analizi).
        /// Model önce bunu yazar; hasarlı parça listesi bununla tutarlı olmalıdır.
        /// </summary>
        public string? SceneDescription { get; set; }
    }

    /// <summary>
    /// Tespit edilen arıza öğesi
    /// </summary>
    public class DiagnosisItemDto
    {
        public string IssueName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProbabilityScore { get; set; } // 0-100
        public string? Category { get; set; } // Motor, Elektrik, Şanzıman, vb.
    }

    /// <summary>
    /// Önerilen parça
    /// </summary>
    public class RecommendedPartDto
    {
        public string PartName { get; set; } = string.Empty;
        public string? PartCode { get; set; }
        public string? Category { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProbabilityScore { get; set; } // 0-100

        // DB eşleştirme sonuçları
        public int? PartId { get; set; }           // DB'de eşleşince part id
        public decimal? RealPrice { get; set; }    // DB'den gerçek fiyat
        public bool IsInDatabase { get; set; }     // true = DB'de bulundu
        public bool IsInStock { get; set; }        // stok > 0
        public int? StockQuantity { get; set; }    // mevcut stok adedi
    }

    /// <summary>
    /// Önerilen işçilik
    /// </summary>
    public class RecommendedLaborDto
    {
        public string LaborName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public decimal? EstimatedHours { get; set; }
        public int ProbabilityScore { get; set; } // 0-100
    }

    /// <summary>
    /// Kaza analizinde hasarlı karoseri parçası
    /// </summary>
    public class DamagedPartDto
    {
        public string PartName { get; set; } = string.Empty;
        public DamageLevel DamageLevel { get; set; }
        public RepairAction RecommendedAction { get; set; }
        public int ConfidencePercent { get; set; }
        public decimal? EstimatedCostMin { get; set; }
        public decimal? EstimatedCostMax { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Kaza analizinde gizli bileşen riski
    /// </summary>
    public class CriticalCheckDto
    {
        public string ComponentName { get; set; } = string.Empty;
        public RiskLevel RiskLevel { get; set; }
        public string Warning { get; set; } = string.Empty;
        public bool RequiresImmediateInspection { get; set; }
    }

    /// <summary>
    /// Kaza onarım maliyet aralığı
    /// </summary>
    public class EstimatedRepairRangeDto
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string Currency { get; set; } = "TRY";
    }
}

