using Microsoft.Extensions.DependencyInjection;
using Wardrobe.Application.Image.BackgroundRemoval.Strategies.Http;

namespace Wardrobe.Application.Image.BackgroundRemoval.Strategies.SharpImage;

public class BackgroundRemovalImageSharpConfigurator
{
    public float BrightnessThreshHold { get; set; }
    
    public BackgroundRemovalImageSharpConfigurator(IServiceCollection collection)
    {
        collection.AddScoped<IBackgroundRemoval, BackgroundRemovalSharpImage>((opt) =>
            new BackgroundRemovalSharpImage(this.BrightnessThreshHold));
    }
}