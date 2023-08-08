using Microsoft.Extensions.Logging;
using Wardrobe.Application.Image.Database;
using Wardrobe.Infra.ML;

namespace Wardrobe.Application.Image.Classification;

public class ClassificationService
{
    private readonly ClassificationPredictionService _classificationPredictionService;
    private readonly IUpdateClassification _updateClassification;
    private readonly ILogger<ClassificationService> _logger;

    public ClassificationService(
        ClassificationPredictionService classificationPredictionService,
        IUpdateClassification updateClassification,
        ILogger<ClassificationService> logger)
    {
        _classificationPredictionService = classificationPredictionService;
        _updateClassification = updateClassification;
        _logger = logger;
    }
    
    public async Task ClassifyImage(string id, string imageBase64, string fileName)
    {
        var tempPath = await SaveBase64Image(imageBase64, fileName);
        var prediction = _classificationPredictionService.Predict(new ImageData { ImagePath = tempPath });
        _logger.LogInformation("Image {FileName} classified as {Label}", fileName, prediction.PredictedLabelValue);
        await _updateClassification.UpdateClassificationAsync(id, prediction.PredictedLabelValue,
            prediction.Score.Average());
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
