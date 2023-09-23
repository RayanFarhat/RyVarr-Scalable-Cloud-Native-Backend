using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Core.Plugins;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ryvarr.models;

namespace RyVarr.Models;

//we need ObservableValidator, it is ObservableProperty and also for data validation,
//and also so when we change project values in the list it is not updated
public partial class Project :  ObservableValidator
{
    [Required]
    [MaxLength(30)]
    [ObservableProperty]
    private string _title="";

    [Required]
    [ObservableProperty]
    private string _description="";

    public DataValidator dataValidator { get; set; }

    public Project(string title, string description)
    {
        Title = title;
        Description = description;

        dataValidator = new DataValidator(this);
        ErrorsChanged += dataValidator.OnError;
    }

    [RelayCommand]
    private void Save()
    {
        ValidateAllProperties();
        dataValidator.Validate();
    }
}
