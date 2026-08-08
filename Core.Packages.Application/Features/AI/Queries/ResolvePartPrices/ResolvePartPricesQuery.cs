using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.ResolvePartPrices
{
    /// <summary>
    /// AI teşhis sonrası çapraz servis envanterinde parça fiyatı çözümleme.
    /// Aracın marka/model/yıl bilgisi + AI'ın önerdiği parça adları ile tüm
    /// aktif servislerin envanteri taranır; servis bazlı fiyat + güven skoru döner.
    /// </summary>
    public class ResolvePartPricesQuery : IRequest<IDataResult<ResolvePartPricesResponse>>
    {
        /// <summary>Araç markası (ör. "BMW").</summary>
        public string VehicleBrand { get; set; } = string.Empty;

        /// <summary>Araç modeli (ör. "3 Serisi").</summary>
        public string VehicleModel { get; set; } = string.Empty;

        /// <summary>Model yılı (ör. 2020).</summary>
        public int VehicleYear { get; set; }

        /// <summary>AI'ın önerdiği parça adları.</summary>
        public List<string> PartNames { get; set; } = new();

        /// <summary>Kullanıcının konumu — yakınlık skoru için (opsiyonel).</summary>
        public double? UserLatitude { get; set; }

        /// <summary>Kullanıcının konumu — yakınlık skoru için (opsiyonel).</summary>
        public double? UserLongitude { get; set; }

        /// <summary>Arama yarıçapı (km). Null = sınırsız.</summary>
        public double? RadiusKm { get; set; }
    }
}
