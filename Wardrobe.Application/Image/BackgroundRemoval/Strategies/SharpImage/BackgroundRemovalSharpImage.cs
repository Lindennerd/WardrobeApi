using Wardrobe.Application.Image.BackgroundRemoval.DTO;
using Wardrobe.Application.Image.BackgroundRemoval.Strategies.Http;
using SixLabors.ImageSharp.Drawing.Processing;

namespace Wardrobe.Application.Image.BackgroundRemoval.Strategies.SharpImage;

public class BackgroundRemovalSharpImage : IBackgroundRemoval
{
    private float BrightnessThreshold { get; set; }
    
    public BackgroundRemovalSharpImage(float brightnessThreshold)
    {
        BrightnessThreshold = brightnessThreshold;
    }    
    public async Task<string> RemoveBackground(BackgroundRemovalDto backgroundRemovalDto)
    {
        var imageBytes = Convert.FromBase64String(backgroundRemovalDto.ImageBase64);
        var imageStream = new MemoryStream(imageBytes);
        
        using var image = await SixLabors.ImageSharp.Image.LoadAsync(imageStream);
        
        var sourceColor = Color.White;
        var targetColor = Color.Transparent;
        var brush = new RecolorBrush(sourceColor, targetColor, threshold: BrightnessThreshold);

        image.Mutate(x => x.Fill(brush));
        
        await using var memoryStream = new MemoryStream();
        await image.SaveAsPngAsync(memoryStream);
        memoryStream.Position = 0;
        return Convert.ToBase64String(memoryStream.ToArray());
    }
}