namespace Wardrobe.Domain.Image;

public abstract class Classification
{
    protected Classification(TemperatureRange temperatureRange, TypeOfClothing typeOfClothing)
    {
        TemperatureRange = temperatureRange;
        TypeOfClothing = typeOfClothing;
    }

    private TemperatureRange TemperatureRange { get; set; }
    private TypeOfClothing TypeOfClothing { get; set; }

    public bool IsMatch(TemperatureRange temperatureRange, TypeOfClothing typeOfClothing)
    {
        return TemperatureRange == temperatureRange && TypeOfClothing == typeOfClothing;
    }
}