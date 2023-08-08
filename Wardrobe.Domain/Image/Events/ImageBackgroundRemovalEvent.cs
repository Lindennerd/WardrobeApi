namespace Wardrobe.Domain.Image.Events;

public interface IImageBackgroundRemovalEvent
{
    public string Id { get; set; }
    public string ImageBase64 { get; set; }
    public string FileName { get; set; }
    public string FileMimeType { get; set; }
}