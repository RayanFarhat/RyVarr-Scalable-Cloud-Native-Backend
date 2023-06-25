using Avalonia.Controls;
using RyVarr.ViewModels;
using Avalonia.Collections;
using System;

namespace RyVarr.Views;

public partial class RyPrimalView : UserControl
{
   public RyPrimalView()
     {
        InitializeComponent();
        var c = this.FindControl<Canvas>("scene");
        if (c != null)
            DataContext = new RyPrimalViewModel(c);
     }
}

