using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using HamzaCad.BarsComputation;
using Autodesk.AutoCAD.Geometry;
using System.Xml.Linq;

namespace HamzaCad
{
    public class FirstBtn
    {
        public RibbonButton item { get; set; }
        public FirstBtn() {
            item = new RibbonButton();
            item.Name = "FirstBtn";
            item.ShowText = true;
            item.Text = "FirstBtn";
            item.CommandHandler = new FirstBtnCommandHandler();
        }
        public class FirstBtnCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter) => true;
            public event EventHandler CanExecuteChanged;
            public void Execute(object parameter)
            {

                Document doc = Application.DocumentManager.MdiActiveDocument;
                Editor ed = doc.Editor;
                BarsComputer.ed = ed;
                Database acCurDb = doc.Database;

                doc.LockDocument();

                Database db = doc.Database;
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {

                    // Open the Block table for read
                    BlockTable acBlkTbl;
                    acBlkTbl = trans.GetObject(acCurDb.BlockTableId,
                                                    OpenMode.ForRead) as BlockTable;

                    // Open the Block table record Model space for write
                    BlockTableRecord acBlkTblRec;
                    acBlkTblRec = trans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                                    OpenMode.ForWrite) as BlockTableRecord;

                    try {
                        // filter selections to only polylines
                        TypedValue[] tv = new TypedValue[1];
                        tv.SetValue(
                            new TypedValue((int)DxfCode.Start, "LWPOLYLINE")   ,0);
                        SelectionFilter filter = new SelectionFilter(tv);
                        //wait for user selection or multible selections
                        PromptSelectionResult ssPrompt = ed.GetSelection(filter);
                        // check if there is object selected
                        if(ssPrompt.Status == PromptStatus.OK)
                        {
                            SelectionSet ss = ssPrompt.Value;
                            foreach (SelectedObject sObj in ss)
                            {
                                // now every time we parse selected polyline here to read its data
                                Polyline p = trans.GetObject(sObj.ObjectId, OpenMode.ForRead) as Polyline;

                                List<DrawingBar> bars = BarsComputer.getBars(p);
                                foreach (DrawingBar bar in bars)
                                {
                                    acBlkTblRec.AppendEntity(bar.Polygon);
                                    trans.AddNewlyCreatedDBObject(bar.Polygon, true);
                                    acBlkTblRec.AppendEntity(bar.Text);
                                    trans.AddNewlyCreatedDBObject(bar.Text, true);
                                }
                            }
                        }
                        else
                        {
                            ed.WriteMessage("No Object selected!");
                        }
                    }
                    catch (System.Exception ex) {
                       ed.WriteMessage("Erorrr:  " + ex.Message);
                    }
                    trans.Commit();
                    trans.Dispose();
                }

            }
        }
    }
}
