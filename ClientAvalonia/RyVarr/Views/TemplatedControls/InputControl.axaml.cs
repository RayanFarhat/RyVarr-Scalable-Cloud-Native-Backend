using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using RyVarr.Tut;

namespace RyVarr.Views.TemplatedControls;

public class InputControl : TemplatedControl
{
    public static readonly StyledProperty<string> LabelNameProperty =
    AvaloniaProperty.Register<LargePanelControl, string>(nameof(LabelName), defaultValue: "Label Name");

    public string LabelName
    {
        get => GetValue(LabelNameProperty);
        set => SetValue(LabelNameProperty, value);
    }

    public static readonly StyledProperty<string> WatermarkTextProperty =
    AvaloniaProperty.Register<LargePanelControl, string>(nameof(WatermarkText), defaultValue: "Enter here");

    public string WatermarkText
    {
        get => GetValue(WatermarkTextProperty);
        set => SetValue(WatermarkTextProperty, value);
    }

    public static readonly StyledProperty<string> InputTextProperty =
    AvaloniaProperty.Register<LargePanelControl, string>(nameof(InputText), defaultValue: "");

    public string InputText
    {
        get => GetValue(InputTextProperty);
        set => SetValue(InputTextProperty, value);
    }
}
