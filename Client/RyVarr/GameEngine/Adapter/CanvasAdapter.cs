using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Controls.Shapes;

namespace RyVarr.GameEngine.Adapter;
/// <summary>
/// Canvas mapper between the game engine and the GUI framework,
/// making the game engine independent of the GUI framework that is used.
/// </summary>
class CanvasAdapter
{
    private Canvas canvas;

    public CanvasAdapter(Canvas canvas) {
        this.canvas = canvas;
        this.canvas.Focusable = true;// when focused it listen to inputs
        this.canvas.Background = Brushes.BurlyWood;
        
    }
    public void SetAxis(Shape obj, double x, double y)
    {
        double centerPointX = (canvas.Width / 2 - obj.Width / 2);
        double centerPointY = (canvas.Height / 2 - obj.Height / 2);

        Canvas.SetLeft(obj, centerPointX + x);
        Canvas.SetTop(obj, centerPointY + y);

    }

    public void Add(GameObjectAdapter obj)
    {
        this.canvas.Children.Add(obj.avaloniaObject);
    }
}
