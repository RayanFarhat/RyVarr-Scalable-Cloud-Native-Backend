using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.Windows;
using HamzaCad.AutoCADAdapter;
using HamzaCad.src.Winforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HamzaCad.FirstBtn;

namespace HamzaCad
{
    public class SecondBtn
    {
        public RibbonButton item { get; set; }
        public SecondBtn()
        {
            item = new RibbonButton();
            item.Name = "Modify Bars";
            item.ShowText = true;
            item.Text = "Modify Bars";
            item.CommandHandler = new FirstBtnCommandHandler();
        }
        public class FirstBtnCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter) => true;
            public event EventHandler CanExecuteChanged;
            public async void Execute(object parameter)
            {
                if (CanExecuteChanged == null)
                {
                }
                Adapter.Init();

                try
                {
                    // filter selections to only polylines
                    TypedValue[] tv = new TypedValue[1];
                    tv.SetValue(
                        new TypedValue((int)DxfCode.Start, "LWPOLYLINE"), 0);
                    SelectionFilter filter = new SelectionFilter(tv);
                    //wait for user selection or multible selections
                    PromptSelectionResult ssPrompt = Adapter.ed.GetSelection(filter);
                    // check if there is object selected
                    if (ssPrompt.Status == PromptStatus.OK)
                    {
                        ModifyBarsWindow s = new ModifyBarsWindow();
                        s.ShowDialog();
                        SelectionSet ss = ssPrompt.Value;
                        foreach (SelectedObject sObj in ss)
                        {
                            // now every time we parse selected polyline here to read its data
                            Polyline p = Adapter.trans.GetObject(sObj.ObjectId, OpenMode.ForWrite) as Polyline;
                            //p.AddVertexAt(3,new Point2d(0,0),0,0,0);
                            BarsModification.BarsEditor.modifyBar(p);
                        }
                    }
                    else
                    {
                        Adapter.ed.WriteMessage("No Object selected!");
                    }
                }
                catch (System.Exception ex)
                {
                    Adapter.ed.WriteMessage("Erorrr:  " + ex.Message + " from " + ex.StackTrace + "\n");
                }
                Adapter.Dispose();
            }
        }
    }
}

