using Wardrobe.Domain.Repository;
using Wardrobe.Infra.Database.Cloth;

namespace Wardrobe.Application.Image.Database;

public class UpdateClassification : IUpdateClassification
{
    private readonly IRepositoryBase<ClothesModel> _repositoryBase;

    public UpdateClassification(IRepositoryBase<ClothesModel> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }
    
    public async Task UpdateClassificationAsync(string id, string classification, float score)
    {
        var cloth = await _repositoryBase.GetByIdAsync(id);
        cloth.Classification = classification;
        cloth.Confidence = score;
        await _repositoryBase.Upsert(id, cloth);
    }
}

public interface IUpdateClassification
{
    Task UpdateClassificationAsync(string id, string classification, float score);
}