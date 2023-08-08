using MassTransit;
using Microsoft.Extensions.Logging;
using Wardrobe.CrossCutting;
using Wardrobe.Domain.Image.Events;

namespace Wardrobe.Application.Image.Classification;

public class ClassificationQueueService : IClassificationQueueService
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly ILogger<ClassificationQueueService> _logger;

    public ClassificationQueueService(ISendEndpointProvider sendEndpointProvider, ILogger<ClassificationQueueService> logger)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _logger = logger;
    }
    
    public async Task SendImageToClassificationQueue(string id, string imageBase64, string fileName, string fileMimeType)
    {
        var endpoint = await this._sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{Queues.Classification.GetDescription()}"));
        await endpoint.Send<IImageClassificationEvent>(new
        {
            Id = id,
            FileName = fileName,
            FileMimeType = fileMimeType,
            ImageBase64 = imageBase64
        });
        
        _logger.LogInformation("Image {FileName} sent to classification queue", fileName);
    }
}

public interface IClassificationQueueService
{
    public Task SendImageToClassificationQueue(string id, string imageBase64, string fileName, string fileMimeType);
}