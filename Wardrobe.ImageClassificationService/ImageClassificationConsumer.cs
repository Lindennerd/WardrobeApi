using MassTransit;
using Wardrobe.Application.Image.Classification;
using Wardrobe.Domain.Image.Events;
using Wardrobe.Infra.Database.Cloth;

namespace Wardrobe.ImageClassificationService;

public class ImageClassificationConsumer : IConsumer<IImageClassificationEvent>
{
    private readonly ClassificationService _classificationService;
    private readonly ILogger<ImageClassificationConsumer> _logger;
    private readonly IClothesRepository _clothesRepository;

    public ImageClassificationConsumer(
        IClothesRepository clothesRepository,
        ClassificationService classificationService,
        ILogger<ImageClassificationConsumer> logger)
    {
        _classificationService = classificationService;
        _clothesRepository = clothesRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<IImageClassificationEvent> context)
    {
        var (id, classification) = await this._classificationService.ClassifyImage(
            context.Message.Id,
            context.Message.ImageBase64,
            context.Message.FileName
        );

        await _clothesRepository.UpdateClassification(id, classification);
    }
}