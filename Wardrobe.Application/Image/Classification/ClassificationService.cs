using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using Wardrobe.Infra.Database.Cloth;
using Wardrobe.Infra.ML;

namespace Wardrobe.Application.Image.Classification;

public class ClassificationService
{
    private readonly IClothesRepository _repository;
    private readonly ILogger<ClassificationService> _logger;
    private readonly PredictionEnginePool<ImageData,ImagePrediction> _classificationPredictionService;

    public ClassificationService(
        IClothesRepository repository,
        PredictionEnginePool<ImageData, ImagePrediction> classificationPredictionService,
        ILogger<ClassificationService> logger)
    {
        _classificationPredictionService = classificationPredictionService;
        _repository = repository;
        _logger = logger;
    }
    
    public async Task ClassifyImage(string id, string imageBase64, string fileName)
    {
        var tempPath = await SaveBase64Image(imageBase64, fileName);
        var prediction = _classificationPredictionService.Predict(new ImageData { ImagePath = tempPath });
        _logger.LogInformation("Image {FileName} classified as {Label}", fileName, prediction.PredictedLabelValue);
        
        await _repository.UpdateClassification(id, prediction.PredictedLabelValue!, prediction.Score!.Max());
        File.Delete(tempPath);
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
