
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RyVarr.ViewModels;
public partial class RyPrimalViewModel : ViewModelBase
{
    public Canvas canvas;
    [ObservableProperty]
    public string greeting="aaaaaa";
    public RyPrimalViewModel(Canvas canvas)
    {
        this.canvas = canvas;
        // Create a rectangle
        var rectangle = new Rectangle
        {
            Width = 50,
            Height = 50,
            Fill = Brushes.White,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        var ellipse = new Ellipse
        {
            Width = 50,
            Height = 50,
            Fill = Brushes.White,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        // Set the position of the rectangle on the canvas
        Canvas.SetLeft(rectangle, 250);
        Canvas.SetTop(rectangle, 250);

        // Add the rectangle to the canvas
        canvas.Children.Add(rectangle);
        canvas.Children.Add(ellipse);

    }


}

