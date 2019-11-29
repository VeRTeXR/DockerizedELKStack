using System;
using System.Text;

namespace LogSplitter {
    class Program {

        private static string startTime = "9/19/2019 09:30:00";
        private static string endTime = "9/19/2019 09:31:00";
        private static System.DateTime parsedStartTime;
        private static string sourcePath = "";
        private static string exitPath = "";
        private static int selectedRow;

        private static StringBuilder csvWriter = new StringBuilder ();

        private static void Main (string[] args) {
            if (args.Length < 3) {
                Console.WriteLine ("usage: dotnet run <sourcePath> <exitPath> <row>");
                return;
            }

            if (args[0] != null)
                sourcePath = args[0];
            if (args[1] != null)
                exitPath = args[1];
            if (args[2] != null) {
                int rowIn = int.Parse (args[2]);
                if (rowIn > 0)
                    selectedRow = rowIn;
                else 
                {
                    Console.WriteLine ("Row not found");
                    return;
                }
            }

            var sourcedStrings = System.IO.File.ReadAllLines (sourcePath);
            var selectedLine = "";

            if (sourcedStrings[selectedRow - 1] == null || selectedRow <= 0) return;
            selectedLine = sourcedStrings[selectedRow - 1];
            var splitedSelectedLine = selectedLine.Split ();

            parsedStartTime = DateTime.Parse (startTime);
            var duration = DateTime.Parse (endTime) - parsedStartTime;
            var tickTime = duration / splitedSelectedLine.Length;

            for (var i = 0; i < splitedSelectedLine.Length; i++) {
                var tickTock = parsedStartTime + (tickTime * i);
                var formatted = tickTock.ToString ("O");
                Console.WriteLine ("tick tock : " + formatted + "," + splitedSelectedLine[i]);
                AppendLineToCsv (splitedSelectedLine, i, formatted);
            }

            System.IO.File.WriteAllText (exitPath, csvWriter.ToString ());
        }

        private static void AppendLineToCsv (string[] splitedString, int i, object tickTock) {
            var newLine = string.Format ("{0},{1}", tickTock, splitedString[i]);
            csvWriter.AppendLine (newLine);
        }
    }
}