using Wardrobe.Domain.Repository;
using Wardrobe.Infra.Database.Cloth;

namespace Wardrobe.Application.Image.Database;

public class SaveImage : ISaveImage
{
    private readonly IRepositoryBase<ClothesModel> _repository;

    public SaveImage(IRepositoryBase<ClothesModel> repository)
    {
        _repository = repository;
    }
    
    public Task<ClothesModel> Save(ClothesModel model)
    {
        return _repository.Save(model);
    }
}

public interface ISaveImage
{
    Task<ClothesModel> Save(ClothesModel model);
}