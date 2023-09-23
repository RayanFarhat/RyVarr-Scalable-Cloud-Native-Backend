using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using RyVarr.Tut;
using RyVarr.ViewModels;

namespace RyVarr.Views.TemplatedControls;

public class CopyableTextControl : TemplatedControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<LargePanelControl, string>(nameof(Text), defaultValue: "Copyable Text");

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public static readonly StyledProperty<string> TextToCopyProperty =
    AvaloniaProperty.Register<LargePanelControl, string>(nameof(Text), defaultValue: "Copyable Text");

    public string TextToCopy
    {
        get => GetValue(TextToCopyProperty);
        set => SetValue(TextToCopyProperty, value);
    }
    public CopyableTextControl()
    {
        DataContext = MainViewModel.GetInstance();
    }
}
