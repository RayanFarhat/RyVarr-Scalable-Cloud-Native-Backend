using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RyVarr.Models;

//we need ObservableProperty, so when we change proect value in the list it is not updated
public partial class Project : ObservableObject
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _description;
    public Project(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
