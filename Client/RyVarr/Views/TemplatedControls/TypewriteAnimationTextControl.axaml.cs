using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using RyVarr.Tut;

namespace RyVarr.Views.TemplatedControls;

public class TypewriteAnimationTextControl : TemplatedControl
{
    public static readonly StyledProperty<string> TextProperty =
       AvaloniaProperty.Register<TypewriteAnimationTextControl, string>(nameof(Text), defaultValue: "The Text");

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}
