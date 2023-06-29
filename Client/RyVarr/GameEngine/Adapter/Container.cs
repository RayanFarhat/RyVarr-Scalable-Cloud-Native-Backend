using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;

namespace RyVarr.GameEngine.Adapter;
/// <summary>
/// Complex Game object where it holds more than one gameobject that can move simulatly
/// </summary>
public class Container : IGameObject<Canvas>
{
    private Canvas _mesh;
    public Canvas Mesh
    {
        get { return _mesh; }
        set { _mesh = value; }
    }
    private double _x;
    public double X
    {
        get { return _x; }
        set { Canvas.SetLeft(Mesh, value - Mesh.Width / 2); _x = value; }
    }
    private double _y;
    public double Y
    {
        get { return _y; }
        set { Canvas.SetTop(Mesh, value - Mesh.Height / 2); _y = value; }
    }
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public Container(string name)
    {
        _name = name;
        _mesh = new Canvas();
    }
    public void AddMeshChild(Shape mesh)
    {
        _mesh.Children.Add(mesh);
    }
}
