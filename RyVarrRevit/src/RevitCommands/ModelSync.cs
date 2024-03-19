using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RyVarrRevit.RC;
using RyVarrRevit.RevitAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RyVarrRevit.RevitCommands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class ModelSync : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData,
          ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Adapter.Init(commandData.Application);
                using (Transaction transaction = new Transaction(Adapter.doc, "Model Synchronization"))
                {
                    transaction.Start();
                    RCModel.Instance.SynchronizeModels();
                    transaction.Commit();
                }
                
            }
            catch (Exception e)
            {
                message = e.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
