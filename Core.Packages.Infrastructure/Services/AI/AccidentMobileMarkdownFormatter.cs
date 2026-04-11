using System.Text;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Kaza/hasar teşhisi için mobil ekranda gösterilecek Markdown özetini üretir
    /// (AI yanıtı gelmezse veya mock kullanılırken).
    /// </summary>
    internal static class AccidentMobileMarkdownFormatter
    {
        public static string BuildFallback(DiagnosisResultDto r)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Kısa net analiz:");
            sb.AppendLine();
            sb.AppendLine("🔧 **Hasar Durumu**");

            if (r.DamagedParts.Count == 0)
                sb.AppendLine("- Görünür panel detayı için fotoğraf veya ek açıklama faydalı olur.");
            else
            {
                foreach (var p in r.DamagedParts)
                {
                    var action = p.RecommendedAction switch
                    {
                        RepairAction.Replace => "değişim",
                        RepairAction.Repair => "düzeltme",
                        RepairAction.Paint => "boya",
                        _ => "değerlendirme"
                    };
                    var dmg = p.DamageLevel switch
                    {
                        DamageLevel.None => "hasar yok",
                        DamageLevel.Light => "hafif",
                        DamageLevel.Medium => "orta",
                        DamageLevel.Heavy => "ağır",
                        DamageLevel.Critical => "kritik",
                        _ => ""
                    };
                    var note = string.IsNullOrWhiteSpace(p.Notes) ? "" : $" ({p.Notes})";
                    sb.AppendLine($"- {p.PartName}: {dmg} → {action}{note}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("⚠️ **Kritik Kontrol (en önemlisi)**");
            if (r.CriticalChecks.Count == 0)
                sb.AppendLine("- Gizli hasar riski nedeniyle radyatör + şase ölçümü önerilir.");
            else
            {
                foreach (var c in r.CriticalChecks)
                    sb.AppendLine($"- {c.ComponentName}");
            }

            sb.AppendLine();
            sb.AppendLine("👉 Bunlar hasarlıysa maliyet uçar");
            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine();
            sb.AppendLine("💰 **Tahmini Masraf (2026 TR)**");
            if (r.EstimatedRepairRange != null)
            {
                var min = r.EstimatedRepairRange.Min;
                var max = r.EstimatedRepairRange.Max;
                sb.AppendLine($"- Toplam aralık (tahmini): **{min:N0} – {max:N0}** {r.EstimatedRepairRange.Currency}");
            }
            else if (r.EstimatedCost.HasValue)
                sb.AppendLine($"- Tahmini toplam (özet): **{r.EstimatedCost:N0}** TRY");
            else
                sb.AppendLine("- Detaylı masraf için ekspertiz gerekir.");

            sb.AppendLine();
            sb.AppendLine("👉 **TOPLAM:**");
            if (r.EstimatedRepairRange != null)
            {
                sb.AppendLine($"- **Minimum:** ~{r.EstimatedRepairRange.Min:N0} TL");
                sb.AppendLine($"- **Maksimum (görünür işler):** ~{r.EstimatedRepairRange.Max:N0} TL");
            }
            sb.AppendLine("- **Şase/radyatör hasarı varsa:** üst banda çıkabilir");

            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine();
            sb.AppendLine("🚨 **Yorum (dürüst)**");
            if (!string.IsNullOrWhiteSpace(r.Recommendations))
                sb.AppendLine(r.Recommendations);
            else
                sb.AppendLine("Hasar şiddeti ve gizli riskler için mutlaka ekspertiz önerilir.");

            sb.AppendLine();
            sb.AppendLine("İstersen:");
            sb.AppendLine("👉 “Pert mi değil mi” net analiz");
            sb.AppendLine("👉 Sigorta / gerçek hasar ayrımı için detaylı kalem listesi");

            return sb.ToString();
        }
    }
}
