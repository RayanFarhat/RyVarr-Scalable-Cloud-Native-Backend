using System;
using Avalonia.Controls;
using Avalonia.Threading;
using RyVarr.DrawEngine.Adapter;

namespace RyVarr.DrawEngine;

public class Engine
{

    private DispatcherTimer gameTimer;
    private DateTime lastTickTime;
    private Canvas _mainCanvas;
    public Canvas MainCanvas
    {
        get { return _mainCanvas; }
        set { _mainCanvas = value; }
    }
    private Scene _scene;
    public Scene Scene
    {
        get { return _scene; }
        set { _scene = value; }
    }
    private double _deltaTime;
    public double DeltaTime
    {
        get { return _deltaTime; }
        set { _deltaTime = value; }
    }

    public Engine(Canvas canvas)
    {
        _mainCanvas = canvas;
        _scene = new Scene(canvas);
        gameTimer = new DispatcherTimer();
        // here gameTick will run 60 time in second (1000/16 = 62.5)
        gameTimer.Interval = TimeSpan.FromMilliseconds(16);
        gameTimer.Tick += Update;
        lastTickTime = DateTime.Now;
    }
    public void Start()
    {
        gameTimer.Start();
    }
    private void Update(object? sender, EventArgs e)
    {
        // ensure consistent player movement regardless of the frame rate
        DateTime currentTime = DateTime.Now;
        DeltaTime = (currentTime - lastTickTime).TotalSeconds;
        lastTickTime = currentTime;
        // now deltaTime should multiplayed with anything that make the objects move
    }
    public void AddUpdateEvent(System.EventHandler func) {
        gameTimer.Tick += func;
    }
    public void Stop()
    {
        gameTimer.Stop();
    }
}
