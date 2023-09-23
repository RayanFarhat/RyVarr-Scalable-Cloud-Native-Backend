using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RyVarr.ViewModels;

public partial class TutViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _channelConfigurationListIsOpen = false;

    [RelayCommand]
    private void ChannelConfigurationBtnPressed() => ChannelConfigurationListIsOpen ^= true;

    [ObservableProperty]
    private AvaloniaList<string> _aalist = new AvaloniaList<string>{"sss", "aaaaaA"};

    public TutViewModel() {
        Aalist.Add("bbb");
    }
}
