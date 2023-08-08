using Microsoft.Extensions.DependencyInjection;

namespace Wardrobe.Application.Image.BackgroundRemoval;

public class BackgroundRemovalServiceConfigurator : IBackgroundRemovalServiceConfigurator
{
    public BackgroundRemovalServiceConfigurator(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }
}

public interface IBackgroundRemovalServiceConfigurator
{
    IServiceCollection Services { get; }
}