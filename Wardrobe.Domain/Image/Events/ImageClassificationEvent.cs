namespace Wardrobe.Domain.Image.Events;

public interface IImageClassificationEvent
{
    public string ImageBase64 { get; set; }
    public string FileName { get; set; }
    public string FileMimeType { get; set; }
    string Id { get; set; }
}