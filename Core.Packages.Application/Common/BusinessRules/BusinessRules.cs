using MagicCarRepairAISupported.Application.Shared.Result;

namespace MagicCarRepairAISupported.Application.Common.BusinessRules
{
    public static class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var result in logics)
            {
                if (!result.Success)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
