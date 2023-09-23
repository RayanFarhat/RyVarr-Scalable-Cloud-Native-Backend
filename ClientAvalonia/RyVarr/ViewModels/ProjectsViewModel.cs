using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RyVarr.Models;

namespace RyVarr.ViewModels;

public partial class ProjectsViewModel : ViewModelBase
{
    [ObservableProperty]
    private AvaloniaList<Project> _projects = new AvaloniaList<Project> { new Project("title","fsadkj fadslj kfnl asdk ljfnadsl"),
    new Project("title2","fsadkjfads ljkfn lasdkljfnadsl"),new Project("title3","fsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer fsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdksltdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdksl"),
        new Project("title4","fsadkjfadsljk fnlasd kljfnadsl"),};

    [RelayCommand]
    private void AddProject(string title)
    {
        Projects.Add(new Project("title here","Description here"));
    }

    [ObservableProperty]
    private Project? _selectedProject = null;

    [RelayCommand]
    private void SaveChanges()
    {
        //send the saves to the backend
    }


    public ProjectsViewModel()
    {
        SelectedProject = Projects.Last();
    }

}
