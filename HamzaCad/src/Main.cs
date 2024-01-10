using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.Windows;
using HamzaCad;

namespace HamzaCAD
{
    public class Main
    {

        [CommandMethod("ry", CommandFlags.Transparent)]
        public void StartLoading()
        {
           MenuManager menuManager = new MenuManager();
            FirstBtn firstBtn = new FirstBtn();
            menuManager.AddItem(firstBtn.item);
            //menuManager.RemoveItem(firstBtn.item);
        }
    }
}
