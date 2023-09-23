using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;
using RyVarr.DrawEngine;

namespace RyVarr.DrawEngine.Adapter;

public class Line : IDrawObject<Avalonia.Controls.Shapes.Line>
{
    private Avalonia.Controls.Shapes.Line _mesh;
    public Avalonia.Controls.Shapes.Line Mesh
    {
        get { return _mesh; }
        set { _mesh = value; }
    }
    private double _x;
    public double X
    {
        get { return _x; }
        set { Canvas.SetLeft(Mesh, value ); _x = value; }
    }
    private double _y;
    public double Y
    {
        get { return _y; }
        set { Canvas.SetTop(Mesh,  - value); _y = value; }
    }
    private string _color;
    public string Color
    {
        get { return _color; }
        set { Mesh.Fill = new SolidColorBrush(Avalonia.Media.Color.Parse(value)); _color = value; }
    }
    public Line(string id, double startX = 0, double startY = 0, double endX = 50, double endY = 50, string color = "#000000FF")
    {
        _mesh = new Avalonia.Controls.Shapes.Line { Tag = id ,StartPoint = new Point(startX,startY), EndPoint = new Point(endX,endY) };
        X = 0;
        Y = 0;
        _color = color;
        Color = color;
    }
}
