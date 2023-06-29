using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;

namespace RyVarr.GameEngine.Adapter;

public class Scene
{
    private Canvas canvas;

    public Scene(Canvas canvas)
    {
        this.canvas = canvas;
        canvas.Name = "Scene";
        this.canvas.Focusable = true;// when focused it listen to inputs
        this.canvas.Background = Brushes.BurlyWood;
        canvas.Width = 500;
        canvas.Height = 400;

    }

    public void AddGameObject(Control mesh)
    {
        this.canvas.Children.Add(mesh);
    }
}
