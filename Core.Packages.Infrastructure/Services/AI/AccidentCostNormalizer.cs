using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// LLM bazen estimatedRepairRange, mobileDisplayMarkdown ve satır bazlı panel aralıklarını birbiriyle çeliştirir.
    /// En az bir panelde min+max varsa toplamları bu satırlardan türetir ve Markdown özetini aynı rakamlarla yeniden üretir.
    /// </summary>
    internal static class AccidentCostNormalizer
    {
        public static bool TryAlignTotalsFromDamagedParts(DiagnosisResultDto r)
        {
            if (r.DiagnosisType != DiagnosisType.Accident || r.DamagedParts.Count == 0)
                return false;

            decimal sumMin = 0, sumMax = 0;
            var withRange = 0;
            foreach (var p in r.DamagedParts)
            {
                if (p.EstimatedCostMin.HasValue && p.EstimatedCostMax.HasValue)
                {
                    sumMin += p.EstimatedCostMin.Value;
                    sumMax += p.EstimatedCostMax.Value;
                    withRange++;
                }
            }

            if (withRange == 0)
                return false;

            var currency = r.EstimatedRepairRange?.Currency;
            if (string.IsNullOrWhiteSpace(currency))
                currency = "TRY";

            r.EstimatedRepairRange = new EstimatedRepairRangeDto
            {
                Min = sumMin,
                Max = sumMax,
                Currency = currency!
            };

            r.EstimatedCost = Math.Round((sumMin + sumMax) / 2m, 0, MidpointRounding.AwayFromZero);

            r.MobileDisplayMarkdown = AccidentMobileMarkdownFormatter.BuildFallback(r);
            return true;
        }
    }
}
