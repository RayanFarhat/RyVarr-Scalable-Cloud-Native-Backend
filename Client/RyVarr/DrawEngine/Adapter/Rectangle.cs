using Avalonia.Media;
using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;

namespace RyVarr.DrawEngine.Adapter;

public class Rectangle : IDrawObject<Avalonia.Controls.Shapes.Rectangle>
{
    private Avalonia.Controls.Shapes.Rectangle _mesh;
    public Avalonia.Controls.Shapes.Rectangle Mesh {
        get { return _mesh; }
        set { _mesh = value; }
    }
    private double _x;
    public double X
    {
        get { return _x; }
        set { Canvas.SetLeft(Mesh, value); _x = value; }
    }
    private double _y;
    public double Y
    {
        get { return _y; }
        set { Canvas.SetTop(Mesh, -value); _y = value; }
    }

    private double _width;
    public double Width
    {
        get { return _width; }
        set { Mesh.Width = value; _width = value; }
    }

    private double _height;
    public double Height
    {
        get { return _height; }
        set { Mesh.Height = value; _height = value; }
    }

    private string _color;
    public string Color
    {
        get { return _color; }
        set { Mesh.Fill = new SolidColorBrush(Avalonia.Media.Color.Parse(value)); _color = value; }
    }

    private string _borderColor;
    public string BorderColor
    {
        get { return _borderColor; }
        set { Mesh.Stroke = new SolidColorBrush(Avalonia.Media.Color.Parse(value)); _borderColor = value; }
    }

    private double _borderSize;
    public double BorderSize
    {
        get { return _borderSize; }
        set { Mesh.StrokeThickness = value; _borderSize = value; }
    }

    public Rectangle(string id, double width = 50, double height = 50, string color = "#000000FF", string borderColor = "#FFFFFFFF", double borderSize = 0)
    {
        _mesh = new Avalonia.Controls.Shapes.Rectangle{Tag = id};
        X = 0;
        Y = 0;
        Width = width;
        Height = height;
        Color = color;
        BorderColor = borderColor;
        BorderSize = borderSize;
    }
}
