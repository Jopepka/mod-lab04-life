using ScottPlot;

namespace Life
{
    public class RenderInScottPlot : IRender
    {
        public void Render(Board board, SettingsRender settingsBoard)
        {
            throw new System.NotImplementedException();
        }

        public void RenderStep(Board board, SettingsRender settingsBoard)
        {
            throw new System.NotImplementedException();
        }

        public void SavePict(Board board, SettingsRender settings, string path)
        {
            var plt = new Plot(settings.WidthMap, settings.HeightMap);

            double[,] cellsInDouble = new double[board.Rows, board.Colums];

            //double[,] cellsInDouble = new double[board.Colums, board.Rows];

            for (int y = 0; y < board.Rows; y++)
                for (int x = 0; x < board.Colums; x++)
                {
                    if (board.Cells[x, y].IsAlive)
                        cellsInDouble[y, x] = 1;
                    //cellsInDouble[x, y] = 1;
                    else
                        cellsInDouble[y, x] = 0;
                    //cellsInDouble[x, y] = 0;
                }

            var hm = plt.AddHeatmap(cellsInDouble, lockScales: false);

            plt.SaveFig(path);
        }
    }
}
