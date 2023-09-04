using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
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
                                int numVertices = p.NumberOfVertices;
                                for (int i = 0; i < numVertices; i++)
                                {
                                    Point2d vertex = p.GetPoint2dAt(i);
                                    ed.WriteMessage($"\nVertex {i + 1}: X={vertex.X}, Y={vertex.Y}");
                                }
                                Line l = new Line(new Point3d(0,0,0),new Point3d(p.GetPoint2dAt(0).X, p.GetPoint2dAt(0).Y,0));
                                acBlkTblRec.AppendEntity(l);
                                trans.AddNewlyCreatedDBObject(l, true);
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
