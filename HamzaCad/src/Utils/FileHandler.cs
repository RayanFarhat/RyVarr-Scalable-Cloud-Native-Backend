using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using HamzaCad.SlabDrawing;

namespace HamzaCad.Utils
{
    public class FileHandler
    {
        public static void writeFile()
        {
            LocalData localData = new LocalData();
            localData.lang = BarsComputer.lang;
            localData.BarSpacing = BarsComputer.BarSpacing;
            localData.drawHorizantal = BarsComputer.drawHorizantal;
            localData.drawVertical = BarsComputer.drawVertical;
            localData.withEar = BarsComputer.withEar;
            localData.earLength = BarsComputer.earLength;
            localData.arrowScale = BarsComputer.arrowScale;
            localData.arrowBlockingLineLength = BarsComputer.arrowBlockingLineLength;
            localData.fontSize = BarsComputer.fontSize;
            localData.iSTopBars = BarsComputer.iSTopBars;
            localData.topBarSymbol = BarsComputer.topBarSymbol;
            localData.bottomrBarSymbol = BarsComputer.bottomrBarSymbol;
            localData.Diameter = BarsComputer.Diameter;
            localData.BarPolySpace = BarsComputer.BarPolySpace;
            localData.upperText = BarsComputer.upperText;
            localData.lowerText = BarsComputer.lowerText;
            localData.MaxBarLength = BarsComputer.MaxBarLength;
            localData.MeetingCircleRadius = BarsComputer.MeetingCircleRadius;
            localData.ironColor = BarsComputer.ironColor;
            localData.IronLineWeight = BarsComputer.IronLineWeight;
            localData.AuthToken = HttpClientHandler.AuthToken;
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
                BarsComputer.BarSpacing = localData.BarSpacing;
                BarsComputer.drawHorizantal = localData.drawHorizantal;
                BarsComputer.drawVertical = localData.drawVertical;
                BarsComputer.withEar = localData.withEar;
                BarsComputer.earLength = localData.earLength;
                BarsComputer.arrowScale = localData.arrowScale;
                BarsComputer.arrowBlockingLineLength = localData.arrowBlockingLineLength;
                BarsComputer.fontSize = localData.fontSize;
                BarsComputer.iSTopBars = localData.iSTopBars;
                BarsComputer.topBarSymbol = localData.topBarSymbol;
                BarsComputer.bottomrBarSymbol = localData.bottomrBarSymbol;
                BarsComputer.Diameter = localData.Diameter;
                BarsComputer.BarPolySpace = localData.BarPolySpace;
                BarsComputer.upperText = localData.upperText;
                BarsComputer.lowerText = localData.lowerText;
                BarsComputer.MaxBarLength = localData.MaxBarLength;
                BarsComputer.MeetingCircleRadius = localData.MeetingCircleRadius;
                BarsComputer.ironColor = localData.ironColor;
                BarsComputer.IronLineWeight = localData.IronLineWeight;
                HttpClientHandler.AuthToken = localData.AuthToken;
            } 
            catch (Exception e) {
                File.Delete(@"localStorage.json");
                BarsComputer.ed.WriteMessage("\nError reading the saved changed!\n");
            }
        }
    }
    public class LocalData
    {
        public string lang { get; set; } = "Eng";

        public double BarSpacing { get; set; } = 30.0;
        public bool drawVertical { get; set; } = true;
        public bool drawHorizantal { get; set; } = true;
        public bool withEar { get; set; } = true;
        public double earLength { get; set; } = 15.0;
        public double arrowScale { get; set; } = 5.0;
        public double arrowBlockingLineLength { get; set; } = 15.0;
        public double fontSize { get; set; } = 15.0;
        public bool iSTopBars { get; set; } = true;
        public string topBarSymbol { get; set; } = "T.B";
        public string bottomrBarSymbol { get; set; } = "B.B";
        public double Diameter { get; set; } = 12.0;
        public double BarPolySpace { get; set; } = 5.0;
        public string upperText { get; set; } = "<>{Q}%%C{D}@{S} {TB}";
        public string lowerText { get; set; } = "L={L}";
        public double MaxBarLength { get; set; } = 800;
        public double MeetingCircleRadius { get; set; } = 3.0;
        public int ironColor { get; set; } = 4;
        public int IronLineWeight { get; set; } = 35;
        public string AuthToken { get; set; } = "";
    }
}
