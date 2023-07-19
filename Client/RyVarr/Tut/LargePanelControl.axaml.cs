using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace RyVarr.Tut;

public class LargePanelControl : TemplatedControl
{
    public static readonly StyledProperty<string> LargeTextProperty =
    AvaloniaProperty.Register<LargePanelControl, string>(nameof(LargeText),defaultValue:"-31.2 LUFS");

    public string LargeText
    {
        get => GetValue(LargeTextProperty);
        set => SetValue(LargeTextProperty, value);
    }
    public static readonly StyledProperty<string> SmallTextProperty =
    AvaloniaProperty.Register<LargePanelControl, string>(nameof(SmallText), defaultValue: "SHORT TERM");

    public string SmallText
    {
        get => GetValue(SmallTextProperty);
        set => SetValue(SmallTextProperty, value);
    }
}
