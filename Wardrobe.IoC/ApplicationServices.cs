using Microsoft.Extensions.DependencyInjection;
using Wardrobe.Application.Image;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.Application.Image.Classification;

namespace Wardrobe.IoC;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}