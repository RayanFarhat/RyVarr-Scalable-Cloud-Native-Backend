using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace RyVarr.Views.TemplatedControls;

public class ErrorListControl : TemplatedControl
{
    public static readonly StyledProperty<List<string>> ErrorMessagesProperty =
    AvaloniaProperty.Register<ErrorListControl, List<string>>(nameof(ErrorMessages), defaultValue: new List<string>());

    public List<string> ErrorMessages
    {
        get => GetValue(ErrorMessagesProperty);
        set => SetValue(ErrorMessagesProperty, value);
    }
    public ErrorListControl()
    {
        ErrorMessages.Add("Error Msg1");
        ErrorMessages.Add("Error Msg2");
        ErrorMessages.Add("Error Msg3");

    }
}
