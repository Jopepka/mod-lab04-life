using System;

namespace Life
{
    public class Board
    {
        public Cell[,] Cells { get; private set; }
        public int Colums { get; private set; }
        public int Rows { get; private set; }
        public int MaxIteration { get; }
        public double LiveDensity { get; private set; }
        public SettingsBoard StartSettings { get; }

        readonly Random rand = new Random();

        public Board(SettingsBoard settings)
        {
            Colums = settings.Colums;
            Rows = settings.Rows;
            MaxIteration = settings.MaxIteration;
            StartSettings = settings;

            Cells = InitilazeCells(Colums, Rows);

            if (settings.BoardStr == null)
            {
                LiveDensity = (double)settings.LiveDensity;
                Cells = CriateRandomizeCells(Colums, Rows, LiveDensity, rand);
            }
            else
            {
                Cells = CriateCellsFromStr(settings.BoardStr);
                LiveDensity = ColculateLiveDensity(Cells);
            }
        }

        public void AddFigure(uint posX, uint posY, SettingsBoard settings)
        {
            if (settings.BoardStr == null)
                throw new ArgumentException("settings.BoardStr shouldn't be null");
            if (posX > Colums || posY > Rows)
                throw new ArgumentException("posX or posY shouldn't be more than Colums or Rows");

            Cell[,] cellsAdd = CriateCellsFromStr(settings.BoardStr);

            for (uint yAbs = posY, yRel = 0; yRel < cellsAdd.GetUpperBound(1) + 1; yAbs++, yRel++)
            {
                if (yAbs >= Rows)
                    yAbs = 0;

                for (uint xAbs = posX, xRel = 0; xRel < cellsAdd.GetUpperBound(0) + 1; xAbs++, xRel++)
                {
                    if (xAbs >= Colums)
                        xAbs = 0;

                    Cells[xAbs, yAbs] = cellsAdd[xRel, yRel];
                }
            }
        }
        public void SaveToJson(string path)
        {
            SettingsBoard settings = new SettingsBoard(Colums, Rows, MaxIteration);
            settings.SetBoardStr(ColculateLiveDensity(Cells));
            settings.SetBoardStr(CriateStrFromCells(Cells));

            SaveJson<SettingsBoard>.SaveToJson(path, settings);
        }

        public static Cell[,] CriateRandomizeCells(int colums, int rows,
            double liveDensity, Random rd)
        {
            Cell[,] cells = InitilazeCells(colums, rows);
            foreach (var cell in cells)
                cell.IsAlive = rd.NextDouble() < liveDensity;
            return cells;
        }

        public void Advance()
        {
            foreach (var cell in Cells)
                cell.DetermineNextLiveState();
            foreach (var cell in Cells)
                cell.Advance();
        }

        private static Cell[,] ConnectNeighbors(Cell[,] cells)
        {
            int colums = cells.GetUpperBound(0) + 1;
            int rows = cells.GetUpperBound(1) + 1;

            for (int x = 0; x < colums; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    int xL = (x > 0) ? x - 1 : colums - 1;
                    int xR = (x < colums - 1) ? x + 1 : 0;

                    int yT = (y > 0) ? y - 1 : rows - 1;
                    int yB = (y < rows - 1) ? y + 1 : 0;

                    cells[x, y].neighbors.Add(cells[xL, yT]);
                    cells[x, y].neighbors.Add(cells[x, yT]);
                    cells[x, y].neighbors.Add(cells[xR, yT]);
                    cells[x, y].neighbors.Add(cells[xL, y]);
                    cells[x, y].neighbors.Add(cells[xR, y]);
                    cells[x, y].neighbors.Add(cells[xL, yB]);
                    cells[x, y].neighbors.Add(cells[x, yB]);
                    cells[x, y].neighbors.Add(cells[xR, yB]);
                }
            }
            return cells;
        }

        private static Cell[,] InitilazeCells(int colums, int rows)
        {
            Cell[,] cells = new Cell[colums, rows];
            for (int x = 0; x < colums; x++)
                for (int y = 0; y < rows; y++)
                    cells[x, y] = new Cell();
            cells = ConnectNeighbors(cells);
            return cells;
        }

        private static double ColculateLiveDensity(Cell[,] cells)
        {
            int colums = cells.GetUpperBound(0) + 1;
            int rows = cells.GetUpperBound(1) + 1;
            int countLive = 0;
            int countEmpty = 0;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < colums; x++)
                {
                    if (cells[x, y].IsAlive)
                        countLive++;
                    else
                        countEmpty++;
                }
            }
            return countLive / countEmpty;
        }

        private Cell[,] CriateCellsFromStr(string str)
        {
            string[] strLines = str.Split('\n');
            Cell[,] cells = InitilazeCells(strLines[0].Length, strLines.Length);
            for (int y = 0; y < strLines.Length; y++)
                for (int x = 0; x < strLines[y].Length; x++)
                {
                    if (strLines[y][x] == '0')
                        cells[x, y].IsAlive = false;
                    else
                        cells[x, y].IsAlive = true;
                }
            return cells;
        }

        private static string CriateStrFromCells(Cell[,] cells)
        {
            int colums = cells.GetUpperBound(0) + 1;
            int rows = cells.GetUpperBound(1) + 1;
            string strLines = "";

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < colums; x++)
                {
                    if (cells[x, y].IsAlive)
                        strLines += '1';
                    else
                        strLines += "0";
                }
                strLines += '\n';
            }
            return strLines;
        }
    }
}
