using System;
using Autodesk.Windows;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;


namespace HamzaCad
{
    public class MenuManager
    {
        public RibbonTab tab { get; set; }
        public RibbonPanelSource panelSrc { get; set; }

        public MenuManager() {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            RibbonControl ribbon = ComponentManager.Ribbon;
            if (ribbon != null)
            {
                tab = ribbon.FindTab("HamzaCAD");
                if (tab != null)
                {
                    ribbon.Tabs.Remove(tab);
                }
                tab = new RibbonTab();
                tab.Title = "HamzaCAD";
                tab.Id = "HamzaCAD";
                ribbon.Tabs.Add(tab);

                // add panel
                panelSrc = new RibbonPanelSource();
                panelSrc.Title = "Panel title";
                RibbonPanel panel = new RibbonPanel();
                panel.Source = panelSrc;
                tab.Panels.Add(panel);

                ed.WriteMessage("Done");
            }
            else
            {
                ed.WriteMessage("failed");
            }
        }
        public void AddItem(RibbonItem item)
        {
            panelSrc.Items.Add(item);
        }
    }
}
