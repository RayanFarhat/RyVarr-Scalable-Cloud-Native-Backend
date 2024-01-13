using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using HamzaCad.SlabDrawing;
using HamzaCad.AutoCADAdapter;
using HamzaCad.DrawingParameters;

namespace HamzaCad.Utils
{
    public class FileHandler
    {
        public static void writeFile()
        {
            LocalData localData = new LocalData();
            localData.lang = BarsComputer.lang;
            localData.BarSpacing = BarsParam.BarSpacing;
            localData.drawHorizantal = BarsParam.DrawHorizontal;
            localData.drawVertical = BarsParam.DrawVertical;
            localData.ArrowSize = DrawingParam.ArrowSize;
            localData.ArrowBlockingLineLength = DrawingParam.ArrowBlockingLineLength;
            localData.TextSize = DrawingParam.TextSize;
            localData.iSTopBars = BarsParam.iSTopBars;
            localData.TopBarSymbol = TextEdirotParam.TopBarSymbol;
            localData.BottomBarSymbol = TextEdirotParam.BottomBarSymbol;
            localData.Diameter = BarsParam.Diameter;
            localData.BarPolySpace = BarsParam.SideCoverY;
            localData.TopText = TextEdirotParam.TopText;
            localData.BottomText = TextEdirotParam.BottomText;
            localData.MaxBarLength = BarsParam.MaxBarLength;
            localData.IntersectCircleRadius = DrawingParam.IntersectCircleRadius;
            localData.IronColor = BarsParam.IronColor;
            localData.IronLineWeight = BarsParam.IronLineWeight;
            string json = JsonSerializer.Serialize(localData);
            File.WriteAllText(@"localStorage.json", json);
        }
        public static void readFileAndApplyChanges()
        {
            try {
                string jsonString = File.ReadAllText(@"localStorage.json");
                LocalData localData;
                localData = JsonSerializer.Deserialize<LocalData>(jsonString);
                BarsComputer.lang = localData.lang;
                BarsParam.BarSpacing = localData.BarSpacing;
                BarsParam.DrawHorizontal = localData.drawHorizantal;
                BarsParam.DrawVertical = localData.drawVertical;
                DrawingParam.ArrowSize = localData.ArrowSize;
                DrawingParam.ArrowBlockingLineLength = localData.ArrowBlockingLineLength;
                DrawingParam.TextSize = localData.TextSize;
                BarsParam.iSTopBars = localData.iSTopBars;
                TextEdirotParam.TopBarSymbol = localData.TopBarSymbol;
                TextEdirotParam.BottomBarSymbol = localData.BottomBarSymbol;
                BarsParam.Diameter = localData.Diameter;
                BarsParam.SideCoverY = localData.BarPolySpace;
                TextEdirotParam.TopText = localData.TopText;
                TextEdirotParam.BottomText = localData.BottomText;
                BarsParam.MaxBarLength = localData.MaxBarLength;
                DrawingParam.IntersectCircleRadius = localData.IntersectCircleRadius;
                BarsParam.IronColor = localData.IronColor;
                BarsParam.IronLineWeight = localData.IronLineWeight;
            } 
            catch  {
                File.Delete(@"localStorage.json");
                Adapter.ed.WriteMessage("\nError reading the saved changed!\n");
            }
        }
    }
    public class LocalData
    {
        public string lang { get; set; } = "Eng";

        public double BarSpacing { get; set; } = 30.0;
        public bool drawVertical { get; set; } = true;
        public bool drawHorizantal { get; set; } = true;
        public double ArrowSize { get; set; } = 5.0;
        public double ArrowBlockingLineLength { get; set; } = 15.0;
        public double TextSize { get; set; } = 15.0;
        public bool iSTopBars { get; set; } = true;
        public string TopBarSymbol { get; set; } = "T.B";
        public string BottomBarSymbol { get; set; } = "B.B";
        public double Diameter { get; set; } = 12.0;
        public double BarPolySpace { get; set; } = 5.0;
        public string TopText { get; set; } = "<>{Q}%%C{D}@{S} {TB}";
        public string BottomText { get; set; } = "L={L}";
        public double MaxBarLength { get; set; } = 800;
        public double IntersectCircleRadius { get; set; } = 3.0;
        public int IronColor { get; set; } = 4;
        public int IronLineWeight { get; set; } = 35;
    }
}
