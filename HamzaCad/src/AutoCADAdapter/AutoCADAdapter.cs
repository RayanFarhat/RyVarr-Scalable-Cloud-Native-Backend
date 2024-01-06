using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using HamzaCad.SlabDrawing;
using System;
using System.Collections.Generic;

namespace HamzaCad.src.AutoCADAdapter
{
    public class AutoCADAdapter
    {
        public static Editor ed;
        public static Transaction trans;
        public static BlockTableRecord blockTableRecord;

        // must call first in btn
        public static void Init()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            ed = doc.Editor;
            doc.LockDocument();
            Database db = doc.Database;
            db.LineWeightDisplay = true;
            trans = db.TransactionManager.StartTransaction();

            // Open the Block table for read
            BlockTable acBlkTbl;
            acBlkTbl = trans.GetObject(db.BlockTableId,
                                            OpenMode.ForRead) as BlockTable;

            // Open the Block table record Model space for write
            blockTableRecord = trans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
                                            OpenMode.ForWrite) as BlockTableRecord;
        }
        // must call last in btn
        public static void Dispose()
        {
            if (trans != null) {
                trans.Commit();
                trans.Dispose(); 
            }
        }

        public static void Add(Entity obj)
        {
            blockTableRecord.AppendEntity(obj);
            trans.AddNewlyCreatedDBObject(obj, true);
        }
        public static void selectObj(Entity obj)
        {
            blockTableRecord.AppendEntity(obj);
            trans.AddNewlyCreatedDBObject(obj, true);
        }
    }
}
