using System;
using System.Collections.Generic;
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
    private AvaloniaList<Project> _list = new AvaloniaList<Project> { new Project("title","fsadkj fadslj kfnl asdk ljfnadsl"),
    new Project("title2","fsadkjfads ljkfn lasdkljfnadsl"),new Project("title3","fsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer fsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdkslfsadkjfa sdfdsf dsfsfrge ret ret ert er ter t ert wt wer tdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdksltdsljkfnl asdkljfnada asdljknasmd;oas asdk;msadmk; asdokns asdksl"),
        new Project("title4","fsadkjfadsljk fnlasd kljfnadsl"),};

    [ObservableProperty]
    private bool _IsProjectEditorOpen = true;
    [RelayCommand]
    private void OpenProjectEditor(string title)
    {
        IsProjectEditorOpen = true;
    }
}
