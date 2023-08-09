using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace RyVarr.Views.TemplatedControls;

public class ErrorListControl : TemplatedControl
{
    public static readonly StyledProperty<ObservableCollection<string>> ErrorMessagesProperty =
    AvaloniaProperty.Register<ErrorListControl, ObservableCollection<string>>(nameof(ErrorMessages), defaultValue: new ObservableCollection<string>());

    public ObservableCollection<string> ErrorMessages
    {
        get => GetValue(ErrorMessagesProperty);
        set => SetValue(ErrorMessagesProperty, value);
    }
    public ErrorListControl()
    {
        ErrorMessages.Clear();
        ErrorMessages.Add("Error Msg1");
        ErrorMessages.Add("Error Msg2");
    }
}
