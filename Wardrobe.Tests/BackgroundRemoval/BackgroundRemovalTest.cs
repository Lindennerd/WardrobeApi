using MongoDB.Bson;
using Wardrobe.Application.Image.BackgroundRemoval.DTO;
using Wardrobe.Application.Image.BackgroundRemoval.Strategies.SharpImage;

namespace Wardrobe.Tests.BackgroundRemoval;

public class BackgroundRemovalTests
{
    private BackgroundRemovalSharpImage _backgroundRemovalSharpImage;

    [SetUp]
    public void Setup()
    {
        _backgroundRemovalSharpImage = new BackgroundRemovalSharpImage(0.5f);
    }

    [Test]
    [TestCase("", "image/png")]
    [TestCase("this is not an image", "image/png")]
    [TestCase("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAA", "")]
    [TestCase("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAA", "image/png")]
    [TestCase(null, null)]
    public async Task Test1(string imageBase64, string mimeType)
    {
        var backgroundRemovalDto = new BackgroundRemovalDto(ObjectId.GenerateNewId().ToString() ?? string.Empty, imageBase64, mimeType);
        var result = await _backgroundRemovalSharpImage.RemoveBackground(backgroundRemovalDto);
        Assert.Pass();
    }
}