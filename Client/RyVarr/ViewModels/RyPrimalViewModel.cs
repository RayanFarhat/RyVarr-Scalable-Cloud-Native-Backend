
using Avalonia.Collections;
using Avalonia.Threading;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Avalonia.Input;

namespace RyVarr.ViewModels;
public partial class RyPrimalViewModel : ViewModelBase
{
    [ObservableProperty]
    public string greeting = "aaaaaa";


    private DispatcherTimer gameTimer;
    private DateTime lastTickTime;


    public Canvas canvas;

    public RyPrimalViewModel(Canvas canvas)
    {

        // here gameTick will run 60 time in second (1000/16 = 62.5)
        gameTimer = new DispatcherTimer();
        gameTimer.Interval = TimeSpan.FromMilliseconds(16);
        gameTimer.Tick += gameTick;
        gameTimer.Start();

        this.canvas = canvas;
        this.canvas.Focusable = true;
        this.canvas.Background = Brushes.BurlyWood;
        canvas.Width = 500;
        canvas.Height = 400;

        // Create a rectangle
        var rectangle = new Rectangle
        {
            Width = 150,
            Height = 50,
            Fill = new SolidColorBrush(Color.Parse("#FF00FF80")),
            Stroke = Brushes.Black,
            StrokeThickness = 2,
            RadiusY = 12,
            RadiusX = 22
        };

        var ellipse = new Ellipse
        {Tag="S",
            Width = 50,
            Height = 50,
            Fill = Brushes.White,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        // Set the position of the rectangle on the canvas
        Canvas.SetLeft(rectangle, canvas.Width/2 - rectangle.Width/2);
        Canvas.SetTop(rectangle, canvas.Height / 2 - rectangle.Height / 2);


        // Add the rectangle to the canvas
        canvas.Children.Add(rectangle);
        canvas.Children.Add(ellipse);
        canvas.KeyDown += keyDown;
        canvas.KeyUp += keyUp;
        canvas.PointerPressed += pointerPressed; 

    }

    private void gameTick(object? sender, EventArgs e)
    {
        // ensure consistent player movement regardless of the frame rate
        DateTime currentTime = DateTime.Now;
        double deltaTime = (currentTime - lastTickTime).TotalSeconds;
        lastTickTime = currentTime;
        // now deltaTime should multiplayed with anything that make the objects move
    }

    private void keyUp(object? sender, EventArgs e)
    {
        Greeting = "vvvvv";
    }

    private void keyDown(object? sender, EventArgs e)
    {
        Greeting = "rrrrrrr";
    }

    private void pointerPressed(object? sender, EventArgs e)
    {
        Greeting ="pppppppppp";
    }
}

