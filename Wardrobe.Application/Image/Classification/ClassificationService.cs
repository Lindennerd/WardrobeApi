using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using Wardrobe.Infra.ML;

namespace Wardrobe.Application.Image.Classification;

public class ClassificationService
{
    private readonly ILogger<ClassificationService> _logger;
    private readonly PredictionEnginePool<ImageData, ImagePrediction> _classificationPredictionService;

    public ClassificationService(
        PredictionEnginePool<ImageData, ImagePrediction> classificationPredictionService,
        ILogger<ClassificationService> logger)
    {
        _classificationPredictionService = classificationPredictionService;
        _logger = logger;
    }

    public async Task<(string id, Domain.Entities.Cloth.Classification classification)> ClassifyImage(string id, string imageBase64, string fileName)
    {
        var tempPath = await SaveBase64Image(imageBase64, fileName);
        var prediction = _classificationPredictionService.Predict(new ImageData { ImagePath = tempPath });
        _logger.LogInformation("Image {FileName} classified as {Label}", fileName, prediction.PredictedLabelValue);

        File.Delete(tempPath);

        return (
            id,
            new Domain.Entities.Cloth.Classification(
                prediction.PredictedLabelValue,
                prediction.Score.Average())
        );
    }

    private async Task<string> SaveBase64Image(string base64Image, string fileName)
    {
        if (!Directory.Exists(Path.Combine(Path.GetTempPath(), "Images")))
            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "Images"));

        var filePath = Path.Combine(Path.GetTempPath(), "Images", fileName);
        var bytes = Convert.FromBase64String(base64Image);
        await File.WriteAllBytesAsync(filePath, bytes);
        return filePath;
    }
}
