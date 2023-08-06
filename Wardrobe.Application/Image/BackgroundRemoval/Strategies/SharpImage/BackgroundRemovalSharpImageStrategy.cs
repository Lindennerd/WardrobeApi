using Wardrobe.Application.Image.BackgroundRemoval.DTO;
using Wardrobe.Application.Image.BackgroundRemoval.Strategies.Http;
using SixLabors.ImageSharp.Drawing.Processing;

namespace Wardrobe.Application.Image.BackgroundRemoval.Strategies.SharpImage;

public class BackgroundRemovalSharpImageStrategy : IBackgroundRemoval
{
    public float BrightnessThreshold { get; set; }
    
    public BackgroundRemovalSharpImageStrategy(float brightnessThreshold)
    {
        BrightnessThreshold = brightnessThreshold;
    }    
    public async Task<string> RemoveBackground(BackgroundRemovalDTO backgroundRemovalDto)
    {
        using var image = await SixLabors.ImageSharp.Image.LoadAsync(backgroundRemovalDto.ImageBase64);
        
        var sourceColor = Color.White;
        var targetColor = Color.Transparent;
        var brush = new RecolorBrush(sourceColor, targetColor, threshold: 0.8F);

        image.Mutate(x => x.Fill(brush));
        
        await using var memoryStream = new MemoryStream();
        await image.SaveAsPngAsync(memoryStream);
        memoryStream.Position = 0;
        return Convert.ToBase64String(memoryStream.ToArray());
    }
}