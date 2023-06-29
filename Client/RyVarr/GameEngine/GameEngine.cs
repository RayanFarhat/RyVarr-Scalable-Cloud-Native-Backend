using System;
using Avalonia.Controls;
using Avalonia.Threading;
using RyVarr.GameEngine.Adapter;

namespace RyVarr.GameEngine;

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

    public Engine(Canvas canvas) {
        _mainCanvas = canvas;
        _scene = new Scene(canvas);
        gameTimer = new DispatcherTimer();
    }
    public void Start()
    {
        // here gameTick will run 60 time in second (1000/16 = 62.5)
        gameTimer = new DispatcherTimer();
        gameTimer.Interval = TimeSpan.FromMilliseconds(16);
        gameTimer.Tick += Update;
        gameTimer.Start();
    }
    private void Update(object? sender, EventArgs e)
    {
        // ensure consistent player movement regardless of the frame rate
        DateTime currentTime = DateTime.Now;
        double deltaTime = (currentTime - lastTickTime).TotalSeconds;
        lastTickTime = currentTime;
        // now deltaTime should multiplayed with anything that make the objects move
    }
    public void Stop()
    {
        gameTimer.Stop();
    }
}
