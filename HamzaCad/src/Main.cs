using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
using Autodesk.Windows;
using HamzaCad;
using HamzaCad.src.Winforms;

namespace HamzaCAD
{
    public class Main
    {

        [CommandMethod("ry", CommandFlags.Transparent)]
        public void StartLoading()
        {
           MenuManager menuManager = new MenuManager();
            FirstBtn firstBtn = new FirstBtn();
            SecondBtn secondBtn = new SecondBtn();
            menuManager.AddItem(firstBtn.item);
            menuManager.AddItem(secondBtn.item);
        }
        [CommandMethod("ryy", CommandFlags.Transparent)]
        public void StartLoading2()
        {
            SlabDrawWindow slabDrawWindow = new SlabDrawWindow();
            slabDrawWindow.Show();
        }
    }
}
