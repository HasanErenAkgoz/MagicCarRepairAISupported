using MagicCarRepairAISupported.Application.Features.AI.Commands.AnalyzeDamagePhotos;

namespace MagicCarRepairAISupported.Application.Tests.Features.AI.AnalyzeDamagePhotos;

public class AnalyzeDamagePhotosCommandHandlerTests
{
    [Fact]
    public async Task Handle_DoesNotDereferenceCallerSuppliedUrlOrLocalPath()
    {
        var handler = new AnalyzeDamagePhotosCommandHandler();
        var result = await handler.Handle(new AnalyzeDamagePhotosCommand
        {
            PhotoPaths = ["https://example.invalid/internal", "/etc/passwd"]
        }, CancellationToken.None);

        Assert.False(result.Success);
    }
}
