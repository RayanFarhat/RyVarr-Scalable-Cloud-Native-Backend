using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI.Events;

namespace RyVarrRevit.RevitAdapter
{
    // used in OnStartup funx inside IExternalApplication class
    public class UIAdapter
    {
        public static UIControlledApplication app;
        public static string tabName; 
        public static List<RibbonPanel> panels = new List<RibbonPanel>();
        public static List<PushButton> PushButtons = new List<PushButton>();
        public static List<TextBox> TextBoxes = new List<TextBox>();
        public static List<RadioButtonGroup> RadioButtonGroups = new List<RadioButtonGroup>(); 
        public static void Init(UIControlledApplication uiControlledApplication)
        {
            app = uiControlledApplication;
        }
        public static void CreateTab(string tabName)
        {
            app.CreateRibbonTab(tabName);
            UIAdapter.tabName = tabName;
        }
        public static void AddPanel(string panelName)
        {
            panels.Add(app.CreateRibbonPanel(tabName,panelName));
        }
        public static void AddPushBtn(int panelIndex,string title,string className, string tooltip = "")
        {
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData(title.Replace(" ",""),
               title, thisAssemblyPath, className);
            PushButton pushButton = panels[panelIndex].AddItem(buttonData) as PushButton;
            if (tooltip != "")
            {
                pushButton.ToolTip = tooltip;
            }
            PushButtons.Add(pushButton);
        }
        // image be 32X32
        public static void AddPushBtnImage(int panelIndex, string title, string className,string imgPath, string tooltip = "")
        {
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData(title.Replace(" ", ""),
               title, thisAssemblyPath, className);
            PushButton pushButton = panels[panelIndex].AddItem(buttonData) as PushButton;
            Uri uriImage = new Uri(imgPath);
            BitmapImage largeImage = new BitmapImage(uriImage);
            pushButton.LargeImage = largeImage;
            if (tooltip != "")
            {
                pushButton.ToolTip = tooltip;
            }
            PushButtons.Add(pushButton);
        }

        // in EnterPressEvent add  void ProcessText(object sender, Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs args){ }
        public static void AddTextBox(int panelIndex, string name,  string PromptText,string tooltip = "", string longDescription = "")
        {
            TextBoxData textData = new TextBoxData(name);
            if (tooltip != "")
            {
                textData.ToolTip = tooltip;
            }
            if (longDescription != "")
            {
                textData.LongDescription = longDescription;
            }
            TextBox tBox = panels[panelIndex].AddItem(textData) as TextBox;
            tBox.PromptText = PromptText;
            TextBoxes.Add(tBox);
        }
        public static void AddRadioButtonGroup( RadioButtonGroup g)
        {
            RadioButtonGroups.Add(g);
        }
    }
}
