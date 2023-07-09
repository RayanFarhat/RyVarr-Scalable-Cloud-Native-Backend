
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Avalonia.Input;
using RyVarr.DrawEngine;

namespace RyVarr.ViewModels;
public partial class RyPrimalViewModel : ViewModelBase
{
    [ObservableProperty]
    public string greeting = "aaaaaa";

    public Engine gameEngine;
    private bool w, d, s, a = false;
    private int p ,z= 0;
    private RyVarr.DrawEngine.Adapter.Rectangle rectangle, rectangle2;

    public RyPrimalViewModel(Canvas canvas)
    {

        gameEngine = new Engine(canvas);
        rectangle = new RyVarr.DrawEngine.Adapter.Rectangle("A", 60,60,"#FF0000FF");
        gameEngine.Scene.AddGameObject(rectangle.Mesh);
        gameEngine.Scene.AddKeyUpEvent(keyUp);
        gameEngine.Scene.AddKeyDownEvent(keyDown); 
        gameEngine.Scene.AddPointerPressedEvent(pointerPressed);
        gameEngine.AddUpdateEvent(Update);
        gameEngine.Start();

        rectangle2 = new RyVarr.DrawEngine.Adapter.Rectangle("B", 10, 10, "#FF0000FF");
        gameEngine.Scene.AddGameObject(rectangle2.Mesh);
    }

    private void Update(object? sender, EventArgs e)
    {
        double speedX = 0;
        double speedY = 0;
        if (w)
        {
            Greeting = "w";
            speedY = 400;
        }
        else if (s)
        {
            Greeting = "s";
            speedY = -400;
        }
        else if (a)
        {
            Greeting = "a";
            speedX = -400;
        }
        else if (d)
        {
            Greeting = "d";
            speedX = 400;
        }
        rectangle.X = rectangle.X + speedX * gameEngine.DeltaTime ;
        rectangle.Y = rectangle.Y + speedY * gameEngine.DeltaTime;
        if (p > 0 && z == 0)
        {
            p--;
            
            rectangle2.X = rectangle.X;
            rectangle2.Y = rectangle.Y;
            
        }
        if(z > 0)
        {
            z--;

            if (rectangle2.X < 400 )
            {
                rectangle2.X = rectangle2.X + 50;
                rectangle2.Y = rectangle2.Y - 50;
            }

        }
    }
    private void keyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            Greeting = "pressing w";
            w = false;

        }
        else if (e.Key == Key.S)
        {
            s = false;
        }
        else if(e.Key == Key.A) { 
            a = false;
        }
        else if(e.Key == Key.D)
        {
            d = false;
        }
       
    }

    private void keyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.W)
        {
            w = true;

        }
        else if (e.Key == Key.S)
        {
            s = true;
        }
        else if (e.Key == Key.A)
        {
            a = true;
        }
        else if (e.Key == Key.D)
        {
            d = true;
        }
        Greeting = "rrrrrrr";
    }

    private void pointerPressed(object? sender, PointerEventArgs e)
    {
        p++;
        z += 10;
        Greeting = "pppppppppp";
    }
}

