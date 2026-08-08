using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace MagicCarRepairAISupported.Application.Tests.TestSupport;

internal static class AutoMapperConfigurationFactory
{
    internal static MapperConfiguration Create(Action<IMapperConfigurationExpression> configure) =>
        new(configure, NullLoggerFactory.Instance);
}
