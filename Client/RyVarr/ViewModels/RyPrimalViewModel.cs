
using Avalonia.Collections;
using Avalonia.Threading;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Avalonia.Input;
using Avalonia;
using System.Reflection.Metadata;
using Avalonia.LogicalTree;
using RyVarr.GameEngine;
using RyVarr.GameEngine.Adapter;
using System.Drawing;

namespace RyVarr.ViewModels;
public partial class RyPrimalViewModel : ViewModelBase
{
    [ObservableProperty]
    public string greeting = "aaaaaa";


    private DispatcherTimer gameTimer;
    private DateTime lastTickTime;


    public Engine gameEngine;

    public RyPrimalViewModel(Canvas canvas)
    {

        // here gameTick will run 60 time in second (1000/16 = 62.5)
        gameTimer = new DispatcherTimer();
        gameTimer.Interval = TimeSpan.FromMilliseconds(16);
        gameTimer.Tick += gameTick;
        gameTimer.Start();
     

        gameEngine = new Engine(canvas);
         RyVarr.GameEngine.Adapter.Rectangle rectangle = new RyVarr.GameEngine.Adapter.Rectangle("A", 60,60,"#FF0000FF");
         gameEngine.Scene.AddGameObject(rectangle.Mesh);


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

