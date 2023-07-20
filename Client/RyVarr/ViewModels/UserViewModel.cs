using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RyVarr.ViewModels;
public partial class UserViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _userName = "User";
}
