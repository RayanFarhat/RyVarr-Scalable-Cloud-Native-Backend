using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using HamzaCad.SlabDrawing;
using HamzaCad.SlabDecomposition;
using HamzaCad.src.AutoCADAdapter;

namespace HamzaCad
{
    public class FirstBtn
    {
        public RibbonButton item { get; set; }
        public FirstBtn() {
            item = new RibbonButton();
            item.Name = "Draw";
            item.ShowText = true;
            item.Text = "Draw";
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
                    AutoCADAdapter.Init();

                    try {
                        // filter selections to only polylines
                        TypedValue[] tv = new TypedValue[1];
                        tv.SetValue(
                            new TypedValue((int)DxfCode.Start, "LWPOLYLINE")   ,0);
                        SelectionFilter filter = new SelectionFilter(tv);
                        //wait for user selection or multible selections
                        PromptSelectionResult ssPrompt = AutoCADAdapter.ed.GetSelection(filter);
                        // check if there is object selected
                        if(ssPrompt.Status == PromptStatus.OK)
                        {
                            SelectionSet ss = ssPrompt.Value;
                            foreach (SelectedObject sObj in ss)
                            {
                                // now every time we parse selected polyline here to read its data
                                Polyline p = AutoCADAdapter.trans.GetObject(sObj.ObjectId, OpenMode.ForRead) as Polyline;

                                List<DrawingBar> bars = await BarsComputer.getBars(p);
                                foreach (DrawingBar bar in bars)
                                {
                                    AutoCADAdapter.Add(bar.Polygon);
                                    AutoCADAdapter.Add(bar.MeetingCircle);

                                    foreach (var text in bar.Texts)
                                    {
                                        AutoCADAdapter.Add(text);
                                    }
                                    foreach (var arrow in bar.Arrows)
                                    {
                                        AutoCADAdapter.Add(arrow);
                                    }
                                    foreach (var line in bar.ArrowsBlockingLines)
                                    {
                                        AutoCADAdapter.Add(line);
                                    }
                                }
                            }
                        }
                        else
                        {
                        AutoCADAdapter.ed.WriteMessage("No Object selected!");
                        }
                    }
                    catch (System.Exception ex) {
                    AutoCADAdapter.ed.WriteMessage("Erorrr:  " + ex.Message + " from "+ex.StackTrace+"\n");
                    }
                    AutoCADAdapter.Dispose();
                }

            }
        }
    }

