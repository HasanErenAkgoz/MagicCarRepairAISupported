using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Help.Queries.GetHelpArticles
{
    public class GetHelpArticlesQuery : IRequest<IDataResult<List<HelpArticleDto>>>
    {
        public string? Category { get; set; }
    }

    public class HelpArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Order { get; set; }
        public int ViewCount { get; set; }
    }

    public class GetHelpArticlesQueryHandler : IRequestHandler<GetHelpArticlesQuery, IDataResult<List<HelpArticleDto>>>
    {
        private readonly Domain.Repositories.EntityFrameworkCore.IHelpArticleRepository _repository;

        public GetHelpArticlesQueryHandler(Domain.Repositories.EntityFrameworkCore.IHelpArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<List<HelpArticleDto>>> Handle(GetHelpArticlesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var articles = await _repository.GetPublishedArticlesAsync(request.Category, cancellationToken);
                
                var dtos = articles.Select(a => new HelpArticleDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    Category = a.Category,
                    Order = a.Order,
                    ViewCount = a.ViewCount
                }).OrderBy(a => a.Order).ThenBy(a => a.Title).ToList();

                return new SuccessDataResult<List<HelpArticleDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<HelpArticleDto>>($"Yardım makaleleri alınırken hata oluştu: {ex.Message}");
            }
        }
    }
}
