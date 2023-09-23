using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.Shapes;

namespace RyVarr.DrawEngine;

/// <summary>
/// Is the global type of the game objects and have the shared things between them
/// X and Y start from the top left in canvas
/// </summary>
public interface IDrawObject<T>
{
    public T Mesh { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}
