using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;
using RyVarr.ViewModels;
namespace RyVarr.Views;

public partial class TutView : UserControl
{
    // all the usecase of all of this codebehind to make the popup follow his btn with the same position
    #region Private Members
    private Control _mChannelConfigurationPopup;
    private Control _mChannelConfigurationBtn;
    private Control _mMainGrid;
    #endregion

    public TutView()
    {
        InitializeComponent();
        DataContext = new TutViewModel();
        _mChannelConfigurationPopup = this.Find<Control>("ChannelConfigurationPopup") ?? throw new System.Exception("Cannot find the control");
        _mChannelConfigurationBtn = this.Find<Control>("ChannelConfigurationBtn") ?? throw new System.Exception("Cannot find the control");
        _mMainGrid = this.Find<Control>("MainGrid") ?? throw new System.Exception("Cannot find the control");

    }
    public override void Render(DrawingContext context)
    {
        base.Render(context);
        // get topleft value of ChannelConfigurationBtn
        var position = _mChannelConfigurationBtn.TranslatePoint(_mChannelConfigurationBtn.Bounds.TopLeft, _mMainGrid) ?? throw new System.Exception("Cannot get point");
        // update the position of popup based on the btn
        Dispatcher.UIThread.Post(() => {
            _mChannelConfigurationPopup.Margin = new Thickness(position.X, 0, 0, _mMainGrid.Bounds.Height - position.Y);
        });
    }

    private void channelOnPointerPressed(object sender, PointerPressedEventArgs args)
    { if (DataContext != null) {
            ((TutViewModel)DataContext).ChannelConfigurationBtnPressedCommand.Execute(null);
      }
        else
        {
            throw new System.Exception("DataContext is null");
        }
    }
        
}
