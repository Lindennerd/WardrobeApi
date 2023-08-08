using Wardrobe.Application.Image.BackgroundRemoval.DTO;

namespace Wardrobe.Application.Image.BackgroundRemoval.Strategies.Http;

public class BackgroundRemovalHttp : IBackgroundRemoval
{
    private readonly IBackgroundRemovalHttpClient _backgroundRemovalHttpClient;

    public BackgroundRemovalHttp(IBackgroundRemovalHttpClient backgroundRemovalHttpClient)
    {
        _backgroundRemovalHttpClient = backgroundRemovalHttpClient;
    }
    
    public async Task<string> RemoveBackground(BackgroundRemovalDto backgroundRemovalDto)
    {
        var base64 = $"data:{backgroundRemovalDto.MimeType};base64,{backgroundRemovalDto.ImageBase64}";
        return await this._backgroundRemovalHttpClient.PostImage(base64);
    }
}

public interface IBackgroundRemoval
{
    Task<string> RemoveBackground(BackgroundRemovalDto backgroundRemovalDto);
}