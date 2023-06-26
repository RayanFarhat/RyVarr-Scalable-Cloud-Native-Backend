using System;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace RyVarr.GameEngine.Adapter;

/// <summary>
/// Resposible for handling the gui interaction part, not the logic
/// </summary>
abstract class GameObjectAdapter
{
    private string Id;
    public Shape avaloniaObject;
    public ShapeType Type;
    public int Width;
    public int Height;

    public GameObjectAdapter(string id, ShapeType type, int width, int height,string color,int borderRadius, string borderColor, int borderThickness)
    {
        Width = width;
        Height = height;
        this.Id = id;
        this.Type = type;
        switch (type)
        {
            case ShapeType.RECTANGLE:
                avaloniaObject = new Rectangle
                {
                    Width = width,
                    Height = height,
                    Fill = new SolidColorBrush(Color.Parse(color)),
                    Stroke = new SolidColorBrush(Color.Parse(borderColor)),
                    StrokeThickness = borderThickness,
                    RadiusX = borderRadius,
                    RadiusY = borderRadius
                };
                break;
            case ShapeType.ELLISPE:
                Console.WriteLine("Number is 2");
                break;
            case ShapeType.LINE:
                Console.WriteLine("Number is 2");
                break;
            case ShapeType.TRIANGLE:
                Console.WriteLine("Number is 2");
                break;
            default:
                break;
        }
    }
}

public enum ShapeType
{
    TRIANGLE,
    RECTANGLE,   
    ELLISPE,
    LINE
}
