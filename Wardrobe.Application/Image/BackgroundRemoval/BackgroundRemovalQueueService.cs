using MassTransit;
using Microsoft.Extensions.Logging;
using Wardrobe.CrossCutting;
using Wardrobe.Domain.Image.Events;

namespace Wardrobe.Application.Image.BackgroundRemoval;

public class BackgroundRemovalQueueService : IBackgroundRemovalQueueService
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly ILogger<BackgroundRemovalQueueService> _logger;

    public BackgroundRemovalQueueService(
        ISendEndpointProvider sendEndpointProvider,
        ILogger<BackgroundRemovalQueueService> logger)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _logger = logger;
    }
    
    public async void SendImageToBackgroundRemovalQueue(string id , string imageBase64, string fileName, string fileMimeType)
    {
        var endpoint = await this._sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{Queues.BackgroundRemoval.GetDescription()}"));
        await endpoint.Send<IImageBackgroundRemovalEvent>(new
        {
            Id = id,
            FileName = fileName,
            FileMimeType = fileMimeType,
            ImageBase64 = imageBase64
        });
        
        _logger.LogInformation("Image {FileName} sent to background removal queue", fileName);
    }
}

public interface IBackgroundRemovalQueueService
{
    public void SendImageToBackgroundRemovalQueue(string id, string imageBase64, string fileName, string fileMimeType);
}