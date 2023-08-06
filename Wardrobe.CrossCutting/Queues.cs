using System.ComponentModel;

namespace Wardrobe.CrossCutting;

public enum Queues
{
    [Description("background-removal")]
    BackgroundRemoval,
    [Description("classification")]
    Classification
}