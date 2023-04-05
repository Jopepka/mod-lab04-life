using System;

namespace Life
{
    public class SettingsBoard
    {
        public int Colums { get; }
        public int Rows { get; }
        public int MaxIteration { get; }
        public double? LiveDensity { get; private set; }
        public string BoardStr { get; private set; }

        public SettingsBoard(int colums = 50, int rows = 20, int maxIteration = 100,
            double? liveDensity = 0.5, string boardStr = null)
        {
            if (liveDensity == null && boardStr == null)
                throw new NullReferenceException("LiveDensity and BoardStr in " +
                "Settings cannot be Null at the same time");

            Colums = colums;
            Rows = rows;
            MaxIteration = maxIteration;
            LiveDensity = liveDensity;
            BoardStr = boardStr;
        }

        public void SetBoardStr(string boardStr)
        {
            string[] strRows = boardStr.Split('\n');
            if (strRows.Length - 1 != Rows)
                throw new ArgumentException($"The number of rows in BoardStr " +
                    $"{strRows.Length} must be equal to Rows {Rows}");

            for (int i = 0; i < strRows.Length - 1; i++)
                if (strRows[i].Length != Colums)
                    throw new ArgumentException($"The number of colums in BoardStr" +
                        $" {strRows[i].Length} must be equal to Colums {Colums}");

            BoardStr = boardStr;
            LiveDensity = null;
        }

        public void SetBoardStr(double liveDensity = 0.5)
        {
            BoardStr = null;
            LiveDensity = liveDensity;
        }
    }
}
