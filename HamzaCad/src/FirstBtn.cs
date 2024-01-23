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
using HamzaCad.AutoCADAdapter;

namespace HamzaCad
{
    public class FirstBtn
    {
        public RibbonButton item { get; set; }
        public FirstBtn() {
            item = new RibbonButton();
            item.Name = "Draw Slab";
            item.ShowText = true;
            item.Text = "Draw Slab";
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

                    try {
                        // filter selections to only polylines
                        TypedValue[] tv = new TypedValue[1];
                        tv.SetValue(
                            new TypedValue((int)DxfCode.Start, "LWPOLYLINE") ,0);
                        SelectionFilter filter = new SelectionFilter(tv);
                        //wait for user selection or multible selections
                        PromptSelectionResult ssPrompt = Adapter.ed.GetSelection(filter);
                        // check if there is object selected
                        if(ssPrompt.Status == PromptStatus.OK)
                        {
                            SelectionSet ss = ssPrompt.Value;
                            foreach (SelectedObject sObj in ss)
                            {

                            // now every time we parse selected polyline here to read its data
                            Polyline p = Adapter.trans.GetObject(sObj.ObjectId, OpenMode.ForRead) as Polyline;

                                List<DrawingBar> bars =  BarsComputer.getBars(p);
                                foreach (DrawingBar bar in bars)
                                {
                                    Adapter.Add(bar.Polygon);
                                    Adapter.Add(bar.MeetingCircle);

                                    ObjectIdCollection arrowList = new ObjectIdCollection();

                                    foreach (var text in bar.Texts)
                                    {
                                        Adapter.Add(text);
                                    }
                                    foreach (var arrow in bar.Arrows)
                                    {
                                        Adapter.Add(arrow);
                                        arrowList.Add(arrow.ObjectId);
                                    }
                                    foreach (var line in bar.ArrowsBlockingLines)
                                    {
                                        Adapter.Add(line);
                                        arrowList.Add(line.ObjectId);
                                    }
                                    Adapter.AddGroup(arrowList);
                            }
                        }
                        }
                        else
                        {
                        Adapter.ed.WriteMessage("No Object selected!");
                        }
                    }
                    catch (System.Exception ex) {
                    Adapter.ed.WriteMessage("Erorrr:  " + ex.Message + " from "+ex.StackTrace+"\n");
                    }
                    Adapter.Dispose();
                }

            }
        }
    }

