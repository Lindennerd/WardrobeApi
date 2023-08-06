using Microsoft.Extensions.Logging;
using Wardrobe.Infra.Database.Cloth;
using Wardrobe.Infra.ML;

namespace Wardrobe.Application.Image.Classification;

public class ClassificationService
{
    private readonly ClassificationPredictionService _classificationPredictionService;
    private readonly ClothesService _clothesService;
    private readonly ILogger<ClassificationService> _logger;

    public ClassificationService(
        ClassificationPredictionService classificationPredictionService,
        ClothesService clothesService, 
        ILogger<ClassificationService> logger)
    {
        _classificationPredictionService = classificationPredictionService;
        _clothesService = clothesService;
        _logger = logger;
    }
    
    public async Task ClassifyImage(string imageBase64, string fileName)
    {
        var tempPath = await SaveBase64Image(imageBase64, fileName);
        var prediction = _classificationPredictionService.Predict(new ImageData { ImagePath = tempPath });
        _logger.LogInformation("Image {FileName} classified as {Label}", fileName, prediction.PredictedLabelValue);
        await SaveClothes(new ClothesModel
        {
            Classification = prediction.PredictedLabelValue,
            Confidence = prediction.Score.Max(),
            Image = imageBase64
        });
        
        File.Delete(tempPath);
    }
    
    private async Task SaveClothes(ClothesModel clothesModel)
    {
        await _clothesService.SaveClothes(clothesModel);
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
