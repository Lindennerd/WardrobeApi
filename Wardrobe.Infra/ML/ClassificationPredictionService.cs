using Microsoft.ML;

namespace Wardrobe.Infra.ML;

public class ClassificationPredictionService
{
    private readonly PredictionEngine<ImageData,ImagePrediction> _predictionEngine;

    public ClassificationPredictionService(string modelPath)
    {
        var mlContext = new MLContext();
        using var file = File.OpenRead(modelPath);
        var model = mlContext.Model.Load(file, out var schema);
        this._predictionEngine = mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
    }

    public ImagePrediction Predict(ImageData imageData)
    {
        return _predictionEngine.Predict(imageData);
    }
}