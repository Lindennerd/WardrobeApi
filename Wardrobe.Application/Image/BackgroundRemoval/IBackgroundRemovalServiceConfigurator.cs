using Microsoft.Extensions.DependencyInjection;

namespace Wardrobe.Application.Image.BackgroundRemoval;

public interface IBackgroundRemovalServiceConfigurator
{
    IServiceCollection Services { get; }
}