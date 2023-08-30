using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RyVarr.Models;

namespace RyVarr.ViewModels;
public partial class PaymentViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPro = false;
    [ObservableProperty]
    private string _paymentLink = "Payment Link";

    [ObservableProperty]
    private bool _showLink = true;

    [ObservableProperty]
    private ObservableCollection<string> _errors = new ObservableCollection<string>();

    [RelayCommand]
    private async Task GetPaymentLink()
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        var res = await clientHandler.Req<IReq>("/api/Payment", "GET", null);
        if (res == null)
        {
            Errors.Clear();
            Errors.Add("Somthing wrong with getting payment data!");
            return;
        }
        if (res.IsSuccessStatusCode)
        {
            PaymentLink = await res.Content.ReadAsStringAsync();
        }
        else
        {
            Errors.Clear();
            Errors.Add($"Got the status {res.StatusCode}");
        }
    }
}