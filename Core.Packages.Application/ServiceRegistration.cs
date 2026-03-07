using MagicCarRepairAISupported.Application.Common.AutoMapper;
using MagicCarRepairAISupported.Application.Common.Behaviors;
using MagicCarRepairAISupported.Application.Common.Services.Translation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
namespace MagicCarRepairAISupported.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCoreApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
            services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddTransient<IMediator, Mediator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // Cache Pipeline Behavior - Uncomment when cache strategy is implemented
            // TODO: Implement cache invalidation strategy before enabling
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachePipelineBehavior<,>));

            // Translation Service
            services.AddScoped<ITranslationService, TranslationService>();

            services.AddAutoMapper(cfg =>
            {
                cfg.DisableConstructorMapping();
            });

            return services;
        }

    }
}
